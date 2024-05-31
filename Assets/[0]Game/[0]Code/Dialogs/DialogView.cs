using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        
        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private MMFeedbacks _feedbacks;
        
        public void SetIcon(Sprite icon)
        {
            _feedbacks.PlayFeedbacks();
            
            if (_icon.sprite == icon)
                return;

            _icon.sprite = icon;
        }

        public void SetText(string text)
        {
            _label.text = text;
        }
    }
}