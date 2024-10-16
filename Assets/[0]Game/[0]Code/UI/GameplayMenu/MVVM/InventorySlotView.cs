using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class InventorySlotView : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _label;

        private ItemConfig _model;
        private InventorySlotViewModel _viewModel;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.Select();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _viewModel.SubmitSlotDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _viewModel.SubmitSlotUp(); 
        }
        
        public void Init(ItemConfig model, InventorySlotViewModel viewModel)
        {
            _model = model;
            _icon.sprite = _model.Icon;
            _viewModel = viewModel;
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