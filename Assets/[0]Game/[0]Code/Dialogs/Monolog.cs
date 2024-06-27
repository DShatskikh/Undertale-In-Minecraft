﻿using System.Collections;
using RimuruDev;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;
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
        private LocalizedString[] _texts;
        private Coroutine _coroutine;
        private string _finallyText;
        private string _currentText;

        private string _continueText;
        private AsyncOperationHandle<string> _continueTextOperation;
        private AsyncOperationHandle<string> _finallyTextOperation;
        
        private IEnumerator Start()
        {
            _continueTextOperation = _continueString.GetLocalizedStringAsync();
            
            while (!_continueTextOperation.IsDone)
            {
                yield return null;
            }
            
            _continueText = _continueTextOperation.Result;
            SetContinueText(_continueText);
        }

        public void Show(LocalizedString[] texts)
        {
            gameObject.SetActive(true);
            GameData.ToMenuButton.gameObject.SetActive(false);
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            GameData.Character.enabled = false;
            var button = _ui.rootVisualElement.Q<Button>("Next_button");
            SetContinueText(_continueText);
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
            
            _finallyTextOperation = _texts[_index].GetLocalizedStringAsync();
            
            while (!_finallyTextOperation.IsDone)
            {
                yield return null;
            }
            
            _finallyText = _finallyTextOperation.Result;
            
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
            if (!_finallyTextOperation.IsDone)
                return;
            
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
            GameData.ToMenuButton.gameObject.SetActive(true);
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

        private void SetContinueText(string text)
        {
            var button = _ui.rootVisualElement.Q<Button>("Next_button");
            button.text = text;
        }
    }
}