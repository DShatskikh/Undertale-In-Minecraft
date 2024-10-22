using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class PuzzleLeverSlotView : BaseSlotController
    {
        [SerializeField]
        private Sprite _leverOn, _leverOff, _leverSelectedOn, _leverSelectedOff;

        [SerializeField]
        private Image _leverImage, _lampImage;

        [SerializeField]
        private PuzzleLeverSlotLever _lever;
        
        private PuzzleLeversView _viewModel;
        private bool _isOn;
        private bool _isSelect;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.SelectSlot(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _viewModel.OnSubmitDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _viewModel.OnSubmitUp();
        }
        
        public void Init(PuzzleLeversView viewModel)
        {
            _viewModel = viewModel;
            _lever.Init(this);
        }

        public override void SetSelected(bool isSelect)
        {
            _isSelect = isSelect;
            Upgrade();
        }

        public void SetOn(bool isOn)
        {
            _isOn = isOn;
            Upgrade();
        }

        private void Upgrade()
        {
            if (_isSelect)
            {
                _leverImage.sprite = _isOn ? _leverSelectedOn : _leverSelectedOff;
            }
            else
            {
                _leverImage.sprite = _isOn ? _leverOn : _leverOff;
            }

            _lampImage.color = _isOn ? Color.white : new Color(0.75f, 0.75f, 0.75f, 1);
        }
    }
}