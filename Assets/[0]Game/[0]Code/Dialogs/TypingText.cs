using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game
{
    public class TypingText : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text _label;
        
        [SerializeField, TextArea]
        private string _text;

        [SerializeField] 
        private AudioSource _audioSource;

        private Coroutine _coroutine;

        private void OnEnable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(TypingProcess());
        }

        private IEnumerator TypingProcess()
        {
            _label.text = "";

            var currentText = "";
            int _countSymbol = 0;

            while (_countSymbol != _text.Length)
            {
                currentText += _text[_countSymbol];
                _audioSource.Play();
                _label.text = currentText;

                for (int i = currentText.Length; i < _text.Length; i++)
                {
                    _label.text += ' ';
                }
                
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
        }
    }
}