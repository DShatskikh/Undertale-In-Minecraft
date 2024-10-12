using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class AllResetKeySlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _frame, _icon;

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
                _frame.color = GameData.AssetProvider.SelectColor;
                _label.color = GameData.AssetProvider.SelectColor;
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
            }
            else
            {
                _frame.color = GameData.AssetProvider.DeselectColor;
                _label.color = GameData.AssetProvider.DeselectColor;
                _icon.sprite = _defaultIcon;
            }
        }
    }
}