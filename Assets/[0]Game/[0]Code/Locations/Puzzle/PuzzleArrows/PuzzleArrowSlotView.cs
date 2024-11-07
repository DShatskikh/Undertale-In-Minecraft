using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class PuzzleArrowSlotView : BaseSlotController, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _arrow;

        [SerializeField]
        private Image _frame;
        
        [SerializeField]
        private Sprite _selectedSprite, _unelectedSprite;

        private PuzzleArrowView _viewModel;
        private bool _isSelect;
        private ArrowDirection _arrowDirection;
        private PuzzleArrowSlotData _model;

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
        
        public void Init(PuzzleArrowView viewModel, PuzzleArrowSlotData model)
        {
            _viewModel = viewModel;
            _arrow.sprite = model.View;
            _model = model;
            //_puzzleButtonSlotButton.Init(this);
        }
        
        public override void SetSelected(bool isSelect)
        {
            _isSelect = isSelect;
            Upgrade();
        }
        
        public void SetDirection(ArrowDirection arrowDirection)
        {
            _arrowDirection = arrowDirection;
            Upgrade();
        }

        private void Upgrade()
        {
            if (_isSelect)
            {
                _frame.sprite = _selectedSprite;
            }
            else
            {
                _frame.sprite = _unelectedSprite;
            }

            _arrow.transform.eulerAngles = _arrowDirection.GetAngle() - _model.StartArrowDirection.GetAngle();
        }
    }
}