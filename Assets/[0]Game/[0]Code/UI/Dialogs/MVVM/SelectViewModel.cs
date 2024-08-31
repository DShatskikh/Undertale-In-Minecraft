using System;
using System.Collections;
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

        private AsyncOperationHandle<string> _yesTextOperation;
        private AsyncOperationHandle<string> _noTextOperation;
        private AsyncOperationHandle<string> _textOperation;

        private SelectModel _model;
        
        public ReactiveProperty<string> YesResultString = new ReactiveProperty<string>();
        public ReactiveProperty<string> NoResultString = new ReactiveProperty<string>();
        public ReactiveProperty<string> Text = new ReactiveProperty<string>();
        public ReactiveProperty<bool> IsEndWrite = new ReactiveProperty<bool>();
        private string _textResultString;
        public event Action Showed;
        public event Action Closed;
        public event Action Write;
        public event Action EndWrite;

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
            _yesTextOperation = _yesString.GetLocalizedStringAsync();
            
            while (!_yesTextOperation.IsDone)
                yield return null;

            _model.YesResultString.Value = _yesTextOperation.Result;

            _noTextOperation = _noString.GetLocalizedStringAsync();
            
            while (!_noTextOperation.IsDone)
                yield return null;

            _model.NoResultString.Value = _noTextOperation.Result;
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
            
            _textOperation = _textLocalization.GetLocalizedStringAsync();
            
            while (!_textOperation.IsDone)
                yield return null;

            _textResultString = _textOperation.Result;
            
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