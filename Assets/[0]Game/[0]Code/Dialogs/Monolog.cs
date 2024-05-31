using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Monolog : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        private int _index;
        private string[] _texts;
        private Coroutine _coroutine;
        private bool _isNotBackCharacter;

        public void Show(string[] texts)
        {
            Show(texts, false);
        }

        public void Show(string[] texts, bool isNotBackCharacter)
        {
            gameObject.SetActive(true);
            _isNotBackCharacter = isNotBackCharacter;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            GameData.Character.enabled = false;
            EventBus.OnSubmit += Next;

            SetText("");
            
            _index = 0;
            _texts = texts;
            Next();
        }

        private IEnumerator TypeText()
        {
            int _countSymbol = 0;
            var text = _texts[_index];
            string currentText = "";

            while (_countSymbol != text.Length)
            {
                if (text[_countSymbol] == '<')
                {
                    while (text[_countSymbol] != '>')
                    {
                        currentText += text[_countSymbol];
                        _countSymbol++;
                    }
                }

                currentText += text[_countSymbol];
                SetText(currentText);
                GameData.TextAudioSource.Play();
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
        }
        
        public void Next()
        {
            GameData.EffectAudioSource.clip = GameData.ClickSound;
            GameData.EffectAudioSource.Play();
            
            if (_index >= _texts.Length)
            {
                Close();
                return;
            }
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(TypeText());
            _index++;
        }

        private void Close()
        {
            EventBus.OnSubmit = null;
            
            if (!_isNotBackCharacter)
                GameData.Character.enabled = true;
            
            gameObject.SetActive(false);
            EventBus.OnCloseMonolog?.Invoke();
            EventBus.OnCloseMonolog = null;
        }

        private void SetText(string text)
        {
            _label.text = text;
        }
    }
}