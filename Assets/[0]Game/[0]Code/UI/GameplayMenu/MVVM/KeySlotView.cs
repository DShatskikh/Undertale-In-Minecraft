using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class KeySlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon, _frame, _resetFrame;
        
        [SerializeField]
        private TMP_Text _nameLabel;

        [SerializeField]
        private TMP_Text _keyLabel, _resetLabel;

        [SerializeField]
        private Sprite _iconSprite;

        [SerializeField]
        private LocalizedString _localizedString;

        public void Init()
        {
            StartCoroutine(AwaitLoad());
        }

        public void SetSelect(bool isSelect, bool isRight)
        {
            if (isSelect)
            {
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
                _nameLabel.color = GameData.AssetProvider.SelectColor;

                if (!isRight)
                {
                    _keyLabel.color = GameData.AssetProvider.SelectColor;
                    _frame.color = GameData.AssetProvider.SelectColor; 
                    _resetLabel.color = GameData.AssetProvider.DeselectColor; 
                    _resetFrame.color = GameData.AssetProvider.DeselectColor;
                }
                else
                {
                    _keyLabel.color = GameData.AssetProvider.DeselectColor;
                    _frame.color = GameData.AssetProvider.DeselectColor; 
                    _resetLabel.color = GameData.AssetProvider.SelectColor; 
                    _resetFrame.color = GameData.AssetProvider.SelectColor;
                }
            }
            else
            {
                _icon.sprite = _iconSprite;
                _nameLabel.color = GameData.AssetProvider.DeselectColor;
                _keyLabel.color = GameData.AssetProvider.DeselectColor;
                _frame.color = GameData.AssetProvider.DeselectColor;
                _resetFrame.color = GameData.AssetProvider.DeselectColor; 
                _frame.color = GameData.AssetProvider.DeselectColor; 
                _resetLabel.color = GameData.AssetProvider.DeselectColor; 
            }
        }
        
        public void UpdateBindingDisplay(InputActionReference m_Action, string m_BindingId)
        {
            var displayString = string.Empty;
            var deviceLayoutName = default(string);
            var controlPath = default(string);

            // Get display string from action.
            var action = m_Action?.action;
            if (action != null)
            {
                var bindingIndex = action.bindings.IndexOf(x => x.id.ToString() == m_BindingId);
                if (bindingIndex != -1)
                    displayString = action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath/*, displayStringOptions*/);
            }
            
            _keyLabel.text = displayString;
        }
        
        private IEnumerator AwaitLoad()
        {
            var loadTextCommand = new LoadTextCommand(_localizedString);
            yield return loadTextCommand.Await().ContinueWith(() => _nameLabel.text = loadTextCommand.Result);
        }
    }
}