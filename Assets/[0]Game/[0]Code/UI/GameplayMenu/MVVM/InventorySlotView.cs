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

        private ItemConfig _model;

        public void Init(ItemConfig model)
        {
            //_icon.sprite = model.Config.Icon;
            _model = model;
            _icon.sprite = _model.Icon;
        }

        public void Upgrade(bool isSelect)
        {
            if (isSelect)
            {
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
                _label.color = GameData.AssetProvider.SelectColor;
            }
            else
            {
                _icon.sprite = _model.Icon;
                _label.color = GameData.AssetProvider.DeselectColor;
            }
        }
    }
}