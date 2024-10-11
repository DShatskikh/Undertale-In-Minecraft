using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HintSlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon, _toggleIcon;

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Sprite _iconSprite;
        
        private HintSlotViewModel _viewModel;
        
        public void Init(HintSlotViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.IsToggle.Changed += IsToggleOnChanged;
        }

        private void OnDestroy()
        {
            _viewModel.IsToggle.Changed -= IsToggleOnChanged;
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

        private void IsToggleOnChanged(bool value)
        {
            _toggleIcon.gameObject.SetActive(value);
        }
    }
}