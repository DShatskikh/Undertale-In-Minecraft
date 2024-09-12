using System;
using System.Collections;
using Febucci.UI;
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
        
        [SerializeField]
        private TextAnimatorPlayer _textAnimatorPlayer;

        private DialogModel _model;
        private Replica[] _replicas;
        private int _indexReplica;
        private bool _isLoad;
        private Coroutine _coroutine;

        public readonly ReactiveProperty<string> Text = new();
        public readonly ReactiveProperty<string> ContinueText = new();
        public readonly ReactiveProperty<bool> IsEndWrite = new();
        public readonly ReactiveProperty<bool> IsShowed = new();
        public readonly ReactiveProperty<Sprite> Icon = new();

        public event Action Write;
        public event Action ShowedAll;

        private void Awake()
        {
            _model = new DialogModel();
            _model.Text.Changed += TextOnChanged;
            _model.IsShowed.Changed += IsShowedOnChanged;
            _model.IsEndWrite.Changed += IsEndWriteOnChanged;
            _model.Icon.Changed += IconOnChanged;
            
            _textAnimatorPlayer.onTextShowed.AddListener(() => _model.IsEndWrite.Value = true);
            _textAnimatorPlayer.onTypewriterStart.AddListener(() => _model.IsEndWrite.Value = false);
            _textAnimatorPlayer.onCharacterVisible.AddListener((c) => Write?.Invoke());
            
            _view.Init(this);
        }

        private void OnDestroy()
        {
            _model.Text.Changed -= TextOnChanged;
            _model.IsShowed.Changed -= IsShowedOnChanged;
            _model.IsEndWrite.Changed -= IsEndWriteOnChanged;
            _model.Icon.Changed -= IconOnChanged;
            
            _textAnimatorPlayer.onTextShowed.RemoveListener(() => _model.IsEndWrite.Value = true);
            _textAnimatorPlayer.onTypewriterStart.RemoveListener(() => _model.IsEndWrite.Value = false);
            _textAnimatorPlayer.onCharacterVisible.RemoveListener((c) => Write?.Invoke());
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
            _coroutine = StartCoroutine(AwaitLoad());
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
            
            IsEndWrite.Value = false;
            _model.Icon.Value = _replicas[_indexReplica].Icon;
            _coroutine = StartCoroutine(AwaitLoad());
        }

        private bool IsEndReplica() => 
            _indexReplica >= _replicas.Length;

        public void ShowAll()
        {
            if (!_isLoad)
                return;
            
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            
            ShowedAll?.Invoke();
        }
        
        private IEnumerator AwaitLoad()
        {
            var loadTextCommand = new LoadTextCommand(_replicas[_indexReplica].LocalizationString);
            yield return loadTextCommand.Await().ContinueWith(() => _model.Text.Value = loadTextCommand.Result);
            _model.Text.Value = loadTextCommand.Result;
            _isLoad = true;
        }

        private void Close()
        {
            _model.IsShowed.Value = false;
            _model.Text.Value = "";
            GameData.ToMenuButton.gameObject.SetActive(true);
            GameData.CharacterController.enabled = true;
            gameObject.SetActive(false);
            EventBus.CloseDialog?.Invoke();
            EventBus.CloseDialog = null;
        }
    }
}