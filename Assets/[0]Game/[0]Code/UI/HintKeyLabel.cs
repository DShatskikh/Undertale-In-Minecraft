using System;
using System.Collections;
using RimuruDev;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Game
{
    public class HintKeyLabel : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference m_Action;

        [SerializeField]
        private string m_BindingId;

        [SerializeField]
        private bool m_isEmptyString;
        
        [SerializeField]
        private LocalizedString m_localizedString;
        
        private TMP_Text _label;
        private string _text;

        private void Awake()
        {
            if (GameData.DeviceType == CurrentDeviceType.Mobile)
                return;
            
            _label = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            GameData.Startup.StartCoroutine(UpdateBindingDisplay());
            LocalizationSettings.SelectedLocaleChanged += LocalizationSettingsOnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= LocalizationSettingsOnSelectedLocaleChanged;
        }

        private void LocalizationSettingsOnSelectedLocaleChanged(Locale obj)
        {
            GameData.Startup.StartCoroutine(UpdateBindingDisplay());
        }

        private IEnumerator UpdateBindingDisplay()
        {
            var displayString = string.Empty;
            
            var action = m_Action?.action;
            if (action != null)
            {
                var bindingIndex = action.bindings.IndexOf(x => x.id.ToString() == m_BindingId);
                if (bindingIndex != -1)
                    displayString = action.GetBindingDisplayString(bindingIndex, out _, out _);
            }
            
            if (!m_isEmptyString)
            {
                var loadTextCommand = new LoadTextCommand(m_localizedString);
                yield return loadTextCommand.Await().ContinueWith(() => 
                    _label.text = $"{loadTextCommand.Result} [{displayString}]");
            }
            else
            {
                _label.text = $"[{displayString}]";
            }
        }
    }
}