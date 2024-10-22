using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class VolumeSlotView : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Slider _slider;
        
        [SerializeField]
        private TMP_Text _valueLabel, _label;

        [SerializeField]
        private Sprite _iconSprite;

        [SerializeField]
        private Image _icon;
        
        private VolumeSlotViewModel _viewModel;
        private bool _isInit;

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
        
        public void Init(VolumeSlotViewModel viewModel)
        {
            _viewModel = viewModel;
            _slider.onValueChanged.AddListener(_viewModel.OnSliderChanged);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(_viewModel.OnSliderChanged);
        }

        public void VolumeOnChanged(float value)
        {
            _slider.value = value;
            _valueLabel.text = $"{(int)(value * 100)}%";
        }

        public void SetSelect(bool isSelect)
        {
            if (isSelect)
            {
                _label.color = GameData.AssetProvider.SelectColor;
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
            }
            else
            {
                _label.color = GameData.AssetProvider.DeselectColor;
                _icon.sprite = _iconSprite;
            }
        }

        public void SubmitDown()
        {
            
        }

        public void SubmitUp()
        {
            
        }
    }
}