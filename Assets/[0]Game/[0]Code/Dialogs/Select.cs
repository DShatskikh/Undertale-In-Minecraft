using System;
using System.Collections;
using RimuruDev;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace Game
{
    public class Select : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _audioSource;

        [SerializeField]
        private UIDocument _ui;
        
        [SerializeField] 
        private AudioClip _clickSound;
        
        [SerializeField]
        private LocalizedString _yesString, _noString;
        
        private LocalizedString _textLocalization;
        private Coroutine _coroutine;
        private Action _yesAction;
        private Action _noAction;

        private string _yesResultString;
        private string _noResultString;
        private string _textResultString;
        
        private AsyncOperationHandle<string> _yesTextOperation;
        private AsyncOperationHandle<string> _noTextOperation;
        private AsyncOperationHandle<string> _textOperation;
        
        private IEnumerator Start()
        {
            _yesTextOperation = _yesString.GetLocalizedStringAsync();
            
            while (!_yesTextOperation.IsDone)
            {
                yield return null;
            }

            _yesResultString = _yesTextOperation.Result;
            var yesButton = _ui.rootVisualElement.Q<Button>("Yes_button");
            yesButton.text = _yesResultString;
            
            _noTextOperation = _noString.GetLocalizedStringAsync();
            
            while (!_noTextOperation.IsDone)
            {
                yield return null;
            }

            _noResultString = _noTextOperation.Result;
            var noButton = _ui.rootVisualElement.Q<Button>("No_button");
            noButton.text = _noResultString;
        }
        
        public void Show(LocalizedString textLocalization, Action yesAction, Action noAction)
        {
            gameObject.SetActive(true);
            GameData.ToMenuButton.gameObject.SetActive(false);
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            GameData.Character.enabled = false;

            _yesAction = yesAction;
            _noAction = noAction;
            
            var yesButton = _ui.rootVisualElement.Q<Button>("Yes_button");
            yesButton.text = _yesResultString;
            EventBus.OnSubmit = SelectTrue;
            yesButton.clicked += SelectTrue;

            var noButton = _ui.rootVisualElement.Q<Button>("No_button");
            noButton.text = _noResultString;
            EventBus.OnCancel = SelectFalse;
            noButton.clicked += SelectFalse;

            if (GameData.DeviceType == CurrentDeviceType.WebMobile)
            {
                _ui.rootVisualElement.Q<Label>("Z").text = "";
                _ui.rootVisualElement.Q<Label>("X").text = "";
            }
            
            _textLocalization = textLocalization;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(TypeText());
        }

        private IEnumerator TypeText()
        {
            int _countSymbol = 0;
            string currentText = "";
            
            _textOperation = _textLocalization.GetLocalizedStringAsync();
            
            while (!_textOperation.IsDone)
            {
                yield return null;
            }
            
            _textResultString = _textOperation.Result;
            
            while (_countSymbol != _textResultString.Length)
            {
                currentText += _textResultString[_countSymbol];
                SetText(currentText);
                _audioSource.Play();
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
        }

        private void SelectTrue()
        {
            EventBus.OnSubmit = null;
            EventBus.OnCancel = null;
            Close();
            _yesAction?.Invoke();
        }
        
        private void SelectFalse()
        {
            EventBus.OnSubmit = null;
            EventBus.OnCancel = null;
            Close();
            _noAction?.Invoke();
        }
        
        private void Close()
        {
            GameData.ToMenuButton.gameObject.SetActive(true);
            GameData.EffectAudioSource.clip = _clickSound;
            GameData.EffectAudioSource.Play();
            gameObject.SetActive(false);
            GameData.Character.enabled = true;
        }

        private void SetText(string text)
        {
            var label = _ui.rootVisualElement.Q<Label>("Label");
            label.text = text;
        }
    }
}