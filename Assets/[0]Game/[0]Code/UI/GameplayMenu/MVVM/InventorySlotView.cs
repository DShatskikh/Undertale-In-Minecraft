using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _label;
        
        public void Init(InventorySlotModel model)
        {
            //_icon.sprite = model.Config.Icon;
        }

        public void Upgrade(bool isSelect, InventorySlotModel model)
        {
            if (isSelect)
            {
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
                _label.color = GameData.AssetProvider.SelectColor;
            }
            else
            {
                _icon.sprite = null;
                _label.color = GameData.AssetProvider.DeselectColor;
            }
        }
    }
}