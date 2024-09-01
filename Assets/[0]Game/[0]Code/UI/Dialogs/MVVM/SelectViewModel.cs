using System;
using System.Collections;
using Game.Commands;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game
{
    public class SelectViewModel : MonoBehaviour
    {
        [SerializeField]
        private LocalizedString _yesString, _noString;

        [SerializeField]
        private SelectView _view;
        
        private LocalizedString _textLocalization;
        private Coroutine _coroutine;
        private Action _yesAction;
        private Action _noAction;
        private string _textResultString;

        private SelectModel _model;
        
        public readonly ReactiveProperty<string> YesResultString = new ReactiveProperty<string>();
        public readonly ReactiveProperty<string> NoResultString = new ReactiveProperty<string>();
        public readonly ReactiveProperty<string> Text = new ReactiveProperty<string>();
        public readonly ReactiveProperty<bool> IsEndWrite = new ReactiveProperty<bool>();
        
        public event Action Showed;
        public event Action Closed;
        public event Action Write;

        private void Awake()
        {
            _model = new SelectModel();
            _model.YesResultString.Changed += YesResultStringChanged;
            _model.NoResultString.Changed += NoResultStringChanged;
            _model.Text.Changed += TextChanged;
            _model.IsEndWrite.Changed += IsEndWriteOnChanged;
            
            _view.Init(this);
        }

        private void OnDestroy()
        {
            _model.YesResultString.Changed -= YesResultStringChanged;
            _model.NoResultString.Changed -= NoResultStringChanged;
            _model.Text.Changed -= TextChanged;
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
            _textLocalization = textLocalization;
            _yesAction = yesAction;
            _noAction = noAction;
            
            gameObject.SetActive(true);
            GameData.ToMenuButton.gameObject.SetActive(false);
            Showed?.Invoke();
            _model.Text.Value = "";
            GameData.CharacterController.enabled = false;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(TypeText());
        }

        public void ShowAll()
        {
            if (_textResultString == null)
                return;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _model.Text.Value = _textResultString;
            _model.IsEndWrite.Value = true;
        }
        
        private IEnumerator TypeText()
        {
            int countSymbol = 0;
            _model.Text.Value = "";
            _textResultString = null;

            var loadTextCommand = new LoadTextCommand(_textLocalization);
            yield return loadTextCommand.Await().ContinueWith(() => _textResultString = loadTextCommand.Result);

            while (countSymbol != _textResultString.Length)
            {
                _model.Text.Value += _textResultString[countSymbol];
                Write?.Invoke();
                yield return new WaitForSeconds(0.05f);
                countSymbol++;
            }
            
            _model.IsEndWrite.Value = true;
        }

        private void NoResultStringChanged(string value)
        {
            NoResultString.Value = value;
        }

        private void TextChanged(string value)
        {
            Text.Value = value;
        }

        private void YesResultStringChanged(string value)
        {
            YesResultString.Value = value;
        }

        private void IsEndWriteOnChanged(bool value)
        {
            IsEndWrite.Value = value;
        }

        public void OnSelectTrue()
        {
            EventBus.Submit = null;
            EventBus.Cancel = null;
            Close();
            _yesAction?.Invoke();
        }

        public void OnSelectFalse()
        {
            EventBus.Submit = null;
            EventBus.Cancel = null;
            Close();
            _noAction?.Invoke();
        }
        
        private void Close()
        {
            GameData.ToMenuButton.gameObject.SetActive(true);
            Closed?.Invoke();
            GameData.CharacterController.enabled = true;
            gameObject.SetActive(false);
        }
    }
}