using System;
using RimuruDev;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace Game
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField] 
        private UIDocument _UI;

        private VisualElement _icon;
        private Label _label;

        private void OnEnable()
        {
            _icon = _UI.rootVisualElement.Q<VisualElement>("Icon");
            _label = _UI.rootVisualElement.Q<Label>("Label");
            
            if (GameData.DeviceType == CurrentDeviceType.WebMobile)
                _UI.rootVisualElement.Q<Label>("Z").text = "";
        }

        public void SetIcon(Sprite icon)
        {
            var back = _icon.style.backgroundImage.value;
            back.sprite = icon;
            _icon.style.backgroundImage = back;
        }

        public void SetText(string text)
        {
            _label.text = text;
        }
    }
}