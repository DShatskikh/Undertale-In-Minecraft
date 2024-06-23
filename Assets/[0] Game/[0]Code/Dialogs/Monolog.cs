using System;
using System.Collections;
using RimuruDev;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace Game
{
    public class Monolog : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;
        
        [SerializeField] 
        private UIDocument _ui;

        [SerializeField] 
        private AudioClip _clickSound;

        [SerializeField]
        private LocalizedString _continueString;
        
        private int _index;
        private string[] _texts;
        private Coroutine _coroutine;
        private string _finallyText;
        private string _currentText;

        public void Show(string[] texts)
        {
            gameObject.SetActive(true);
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            GameData.Character.enabled = false;
            var button = _ui.rootVisualElement.Q<Button>("Next_button");
            button.text = _continueString.GetLocalizedString();
            button.clicked += Next;
            EventBus.OnSubmit = Next;
            EventBus.OnCancel = ShowFinallyText;

            if (GameData.DeviceType == CurrentDeviceType.WebMobile)
                _ui.rootVisualElement.Q<Label>("Z").text = "";
            
            SetText("");
            
            _index = 0;
            _texts = texts;
            Next();
        }

        private IEnumerator TypeText()
        {
            int _countSymbol = 0;
            _finallyText = _texts[_index];
            _currentText = "";

            while (_countSymbol != _finallyText.Length)
            {
                if (_finallyText[_countSymbol] == '<')
                {
                    while (_finallyText[_countSymbol] != '>')
                    {
                        _currentText += _finallyText[_countSymbol];
                        _countSymbol++;
                    }
                }

                _currentText += _finallyText[_countSymbol];
                SetText(_currentText);
                _audioSource.Play();
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
        }

        private void Next()
        {
            if (_currentText != _finallyText)
            {
                ShowFinallyText();
                return;
            }
            
            GameData.EffectAudioSource.clip = _clickSound;
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

        private void ShowFinallyText()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _currentText = _finallyText;
            SetText(_finallyText);
            print("ShowFinallyText");
        }
        
        private void Close()
        {
            EventBus.OnSubmit = null;
            EventBus.OnCancel = null;
            GameData.Character.enabled = true;
            gameObject.SetActive(false);
            EventBus.OnCloseMonolog?.Invoke();
            EventBus.OnCloseMonolog = null;
        }

        private void SetText(string text)
        {
            var label = _ui.rootVisualElement.Q<Label>("Label");
            label.text = text;
        }
    }
}