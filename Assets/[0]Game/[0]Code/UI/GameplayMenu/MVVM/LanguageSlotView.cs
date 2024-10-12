using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LanguageSlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Sprite _defaultIcon;
        
        public void Init()
        {
            
        }

        public void Upgrade(bool isSelect)
        {
            if (isSelect)
            {
                _label.color = GameData.AssetProvider.SelectColor;
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
            }
            else
            {
                _label.color = GameData.AssetProvider.DeselectColor;
                _icon.sprite = _defaultIcon;
            }
        }
    }
}