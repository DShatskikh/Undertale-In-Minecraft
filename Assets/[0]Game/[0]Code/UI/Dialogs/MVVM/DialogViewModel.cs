using System;
using System.Collections;
using Game.Commands;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class DialogViewModel : MonoBehaviour
    {
        [SerializeField] 
        private DialogView _view;

        [SerializeField]
        private LocalizedString _continueString;

        private DialogModel _model;
        private Replica[] _replicas;
        private int _indexReplica;
        private Coroutine _coroutine;
        private string _finallyText;

        public readonly ReactiveProperty<string> Text = new ReactiveProperty<string>();
        public readonly ReactiveProperty<string> ContinueText = new ReactiveProperty<string>();
        public readonly ReactiveProperty<bool> IsEndWrite = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> IsShowed = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<Sprite> Icon = new ReactiveProperty<Sprite>();

        public event Action LoadText;
        public event Action Write;
        public event Action Next;

        private void Awake()
        {
            _model = new DialogModel();
            _model.Text.Changed += TextOnChanged;
            _model.IsShowed.Changed += IsShowedOnChanged;
            _model.IsEndWrite.Changed += IsEndWriteOnChanged;
            _model.Icon.Changed += IconOnChanged;
            
            _view.Init(this);
        }

        private void OnDestroy()
        {
            _model.Text.Changed -= TextOnChanged;
            _model.IsShowed.Changed -= IsShowedOnChanged;
            _model.IsEndWrite.Changed -= IsEndWriteOnChanged;
            _model.Icon.Changed -= IconOnChanged;
        }

        private IEnumerator Start()
        {
            var loadTextCommand = new LoadTextCommand(_continueString);
            yield return loadTextCommand.Await().ContinueWith(() => ContinueText.Value = loadTextCommand.Result);
        }

        public void Show(Replica[] replicas, AudioClip sound = null)
        {
            gameObject.SetActive(true);
            _model.IsShowed.Value = true;
            _view.SetSound(sound);
            GameData.ToMenuButton.gameObject.SetActive(false);

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            GameData.CharacterController.enabled = false;
            _replicas = replicas;
            _indexReplica = 0;
            _model.Text.Value = "";
            _model.Icon.Value = _replicas[_indexReplica].Icon;
            _coroutine = StartCoroutine(TypeText());
        }

        private void IsShowedOnChanged(bool value)
        {
            IsShowed.Value = value;
        }

        private void TextOnChanged(string value)
        {
            Text.Value = value;
        }

        private void IsEndWriteOnChanged(bool value)
        {
            IsEndWrite.Value = value;
        }
        
        private void IconOnChanged(Sprite value)
        {
            Icon.Value = value;
        }

        public void TryNext()
        {
            _indexReplica++;
            
            if (IsEndReplica())
            {
                Close();
                return;
            }

            Next?.Invoke();
            IsEndWrite.Value = false;
            _model.Text.Value = "";
            _model.Icon.Value = _replicas[_indexReplica].Icon;
            _coroutine = StartCoroutine(TypeText());
        }

        private bool IsEndReplica() => 
            _indexReplica >= _replicas.Length;

        public void ShowAllText()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            var replica = _replicas[_indexReplica];
            _model.Text.Value = _finallyText;
            _model.Icon.Value = replica.Icon;
            IsEndWrite.Value = true;
        }
        
        private IEnumerator TypeText()
        {
            _model.Text.Value = "";
            int _countSymbol = 0;
            
            var loadTextCommand = new LoadTextCommand(_replicas[_indexReplica].LocalizationString);
            yield return loadTextCommand.Await().ContinueWith(() => _finallyText = loadTextCommand.Result);
            
            LoadText?.Invoke();
            
            while (_countSymbol != _finallyText.Length)
            {
                if (_finallyText[_countSymbol] == '<')
                {
                    while (_finallyText[_countSymbol] != '>')
                    {
                        _model.Text.Value += _finallyText[_countSymbol];
                        _countSymbol++;
                    }
                }

                _model.Text.Value += _finallyText[_countSymbol];
                Write?.Invoke();
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
            
            IsEndWrite.Value = true;
        }

        private void Close()
        {
            print("Close");
            _model.IsShowed.Value = false;
            GameData.ToMenuButton.gameObject.SetActive(true);
            EventBus.Submit = null;
            EventBus.Cancel = null;
            GameData.CharacterController.enabled = true;
            gameObject.SetActive(false);
            EventBus.CloseDialog?.Invoke();
            EventBus.CloseDialog = null;
        }
    }
}