using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game
{
    public class TypingText : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text _label;
        
        [SerializeField, TextArea]
        private string _text;

        [SerializeField]
        private LocalizedString _localizationString;
        
        [SerializeField] 
        private AudioSource _audioSource;

        private Coroutine _coroutine;
        private AsyncOperationHandle<string> _operation;
        private string _resultText = string.Empty;

        public string GetText => _text;
        public LocalizedString SetLocalizationString
        {
            set { _localizationString = value; }
        }

        private void OnEnable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(TypingProcess());

            EventBus.OnCancel = ShowAllText;
        }

        private void ShowAllText()
        {
            if (!_operation.IsDone)
                return;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _label.text = _resultText;
        }
        
        private IEnumerator TypingProcess()
        {
            _label.text = "";
            _operation = _localizationString.GetLocalizedStringAsync();
            
            var currentText = "";
            int _countSymbol = 0;

            while (!_operation.IsDone)
            {
                yield return null;
            }
            
            _resultText = _operation.Result;
            var length = _resultText.Length;
            
            while (_countSymbol != length)
            {
                currentText += _resultText[_countSymbol];
                _audioSource.Play();
                _label.text = currentText;

                for (int i = currentText.Length; i < length; i++)
                {
                    _label.text += ' ';
                }
                
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
        }
    }
}