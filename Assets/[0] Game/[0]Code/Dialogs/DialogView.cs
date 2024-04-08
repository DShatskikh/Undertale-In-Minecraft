using UnityEngine;
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