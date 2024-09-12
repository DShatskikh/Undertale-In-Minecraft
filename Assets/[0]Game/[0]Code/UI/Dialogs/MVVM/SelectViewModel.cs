using System;
using System.Collections;
using Febucci.UI;
using Game.Commands;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class SelectViewModel : MonoBehaviour
    {
        [SerializeField]
        private LocalizedString _yesString, _noString;

        [SerializeField]
        private SelectView _view;
        
        [SerializeField]
        private TextAnimatorPlayer _textAnimatorPlayer;
        
        private LocalizedString _textLocalization;
        private Coroutine _coroutine;
        private Action _yesAction;
        private Action _noAction;
        private bool _isLoad;
        private SelectModel _model;
        
        public readonly ReactiveProperty<string> YesResultString = new();
        public readonly ReactiveProperty<string> NoResultString = new();
        public readonly ReactiveProperty<string> Text = new();
        public readonly ReactiveProperty<bool> IsEndWrite = new();
        public readonly ReactiveProperty<bool> IsShowed = new();
        
        public event Action Write;
        public event Action ShowedAll;

        private void Awake()
        {
            _model = new SelectModel();
            _model.YesResultString.Changed += YesResultStringChanged;
            _model.NoResultString.Changed += NoResultStringChanged;
            _model.Text.Changed += TextChanged;
            _model.IsEndWrite.Changed += IsEndWriteOnChanged;
            
            _textAnimatorPlayer.onTextShowed.AddListener(() => _model.IsEndWrite.Value = true);
            _textAnimatorPlayer.onTypewriterStart.AddListener(() => _model.IsEndWrite.Value = false);
            _textAnimatorPlayer.onCharacterVisible.AddListener((c) => Write?.Invoke());
            
            _view.Init(this);
        }

        private void OnDestroy()
        {
            _model.YesResultString.Changed -= YesResultStringChanged;
            _model.NoResultString.Changed -= NoResultStringChanged;
            _model.Text.Changed -= TextChanged;
            _model.IsEndWrite.Changed -= IsEndWriteOnChanged;
            
            _textAnimatorPlayer.onTextShowed.RemoveListener(() => _model.IsEndWrite.Value = true);
            _textAnimatorPlayer.onTypewriterStart.RemoveListener(() => _model.IsEndWrite.Value = false);
            _textAnimatorPlayer.onCharacterVisible.RemoveListener((c) => Write?.Invoke());
        }

        private IEnumerator Start()
        {
            var loadTextCommand = new LoadTextCommand(_yesString);
            yield return loadTextCommand.Await().ContinueWith(() => _model.YesResultString.Value = loadTextCommand.Result);

            loadTextCommand = new LoadTextCommand(_noString);
            yield return loadTextCommand.Await().ContinueWith(() => _model.NoResultString.Value = loadTextCommand.Result);
        }

        public void Show(LocalizedString textLocalization, Action yesAction, Action noAction)
        {
            _isLoad = false;
            _textLocalization = textLocalization;
            _yesAction = yesAction;
            _noAction = noAction;
            
            gameObject.SetActive(true);
            GameData.ToMenuButton.gameObject.SetActive(false);
            IsShowed.Value = true;
            _model.Text.Value = "";
            GameData.CharacterController.enabled = false;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(AwaitLoad());
        }

        public void ShowAll()
        {
            if (!_isLoad)
                return;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            ShowedAll?.Invoke();
        }
        
        private IEnumerator AwaitLoad()
        {
            var loadTextCommand = new LoadTextCommand(_textLocalization);
            yield return loadTextCommand.Await().ContinueWith(() => _model.Text.Value = loadTextCommand.Result);
        }

        private void NoResultStringChanged(string value)
        {
            NoResultString.Value = value;
        }

        private void TextChanged(string value)
        {
            Text.Value = value;
            _isLoad = true;
        }

        private void YesResultStringChanged(string value)
        {
            YesResultString.Value = value;
        }

        private void IsEndWriteOnChanged(bool value)
        {
            IsEndWrite.Value = value;
        }

        public void OnSelectYes()
        {
            Close();
            _yesAction?.Invoke();
        }

        public void OnSelectNo()
        {
            Close();
            _noAction?.Invoke();
        }
        
        private void Close()
        {
            _model.Text.Value = "";
            GameData.ToMenuButton.gameObject.SetActive(true);
            IsShowed.Value = false;
            GameData.CharacterController.enabled = true;
            gameObject.SetActive(false);
        }
    }
}