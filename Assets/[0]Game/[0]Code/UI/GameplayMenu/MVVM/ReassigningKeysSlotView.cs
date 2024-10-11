using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ReassigningKeysSlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Sprite _iconSprite;

        private ReassigningKeysSlotViewModel _viewModel;
        
        public void Init(ReassigningKeysSlotViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void SetSelect(bool isSelect)
        {
            if (isSelect)
            {
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
                _label.color = GameData.AssetProvider.SelectColor;
            }
            else
            {
                _icon.sprite = _iconSprite;
                _label.color = GameData.AssetProvider.DeselectColor;
            }
        }
    }
}