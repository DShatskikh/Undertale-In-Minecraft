using System;
using System.Collections;
using RimuruDev;
using UnityEngine;
using UnityEngine.Localization;
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
        
        private string _text;
        private Coroutine _coroutine;
        private Action _yesAction;
        private Action _noAction;
        
        public void Show(string text, Action yesAction, Action noAction)
        {
            gameObject.SetActive(true);
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            GameData.Character.enabled = false;

            _yesAction = yesAction;
            _noAction = noAction;
            
            var yesButton = _ui.rootVisualElement.Q<Button>("Yes_button");
            yesButton.text = _yesString.GetLocalizedString();
            EventBus.OnSubmit = SelectTrue;
            yesButton.clicked += SelectTrue;

            var noButton = _ui.rootVisualElement.Q<Button>("No_button");
            noButton.text = _noString.GetLocalizedString();
            EventBus.OnCancel = SelectFalse;
            noButton.clicked += SelectFalse;

            if (GameData.DeviceType == CurrentDeviceType.WebMobile)
            {
                _ui.rootVisualElement.Q<Label>("Z").text = "";
                _ui.rootVisualElement.Q<Label>("X").text = "";
            }
            
            _text = text;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(TypeText());
        }

        private IEnumerator TypeText()
        {
            int _countSymbol = 0;
            string currentText = "";

            while (_countSymbol != _text.Length)
            {
                currentText += _text[_countSymbol];
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