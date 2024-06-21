using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

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
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _label.text = _localizationString.GetLocalizedString();
        }
        
        private IEnumerator TypingProcess()
        {
            _label.text = "";

            var currentText = "";
            int _countSymbol = 0;

            var text = _localizationString.GetLocalizedString();
            var length = text.Length;
            
            while (_countSymbol != length)
            {
                currentText += text[_countSymbol];
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