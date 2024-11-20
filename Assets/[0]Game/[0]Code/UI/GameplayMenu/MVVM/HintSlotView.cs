using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class HintSlotView : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _icon, _toggleIcon;

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Sprite _iconSprite;
        
        private HintSlotViewModel _viewModel;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.Select();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _viewModel.SubmitDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _viewModel.Use(); 
        }
        
        public void Init(HintSlotViewModel viewModel)
        {
            _viewModel = viewModel;
            GameData.SettingStorage.IsShowHint.Changed += IsToggleOnChanged;
        }

        private void OnDestroy()
        {
            GameData.SettingStorage.IsShowHint.Changed -= IsToggleOnChanged;
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

        public void IsToggleOnChanged(bool value)
        {
            _toggleIcon.gameObject.SetActive(value);
        }
    }
}