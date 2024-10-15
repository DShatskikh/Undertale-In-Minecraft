using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class LanguageSlotView : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        private Image _icon, _frame;

        [SerializeField]
        private TMP_Text _label, _dropDownLabel;

        [SerializeField]
        private Sprite _defaultIcon;

        private LanguageSlotViewModel _viewModel;
        
        public void Init(LanguageSlotViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Upgrade(bool isSelect, bool isShow)
        {
            if (isSelect)
            {
                _icon.sprite = GameData.AssetProvider.CharacterIcon;

                if (isShow)
                {
                    _frame.color = GameData.AssetProvider.DeselectColor;
                    _dropDownLabel.color = GameData.AssetProvider.DeselectColor;
                    _label.color = GameData.AssetProvider.DeselectColor;
                }
                else
                {
                    _frame.color = GameData.AssetProvider.SelectColor;
                    _dropDownLabel.color = GameData.AssetProvider.SelectColor;  
                    _label.color = GameData.AssetProvider.SelectColor;
                }
            }
            else
            {
                _label.color = GameData.AssetProvider.DeselectColor;
                _icon.sprite = _defaultIcon;
                _frame.color = GameData.AssetProvider.DeselectColor;
                _dropDownLabel.color = GameData.AssetProvider.DeselectColor;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.Select();
        }
    }
}