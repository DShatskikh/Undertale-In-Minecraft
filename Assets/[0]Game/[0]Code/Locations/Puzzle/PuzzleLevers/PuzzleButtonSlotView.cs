using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class PuzzleButtonSlotView : BaseSlotController
    {
        [SerializeField]
        private Sprite _selectedSprite, _unelectedSprite;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private PuzzleButtonSlotButton _puzzleButtonSlotButton;
        
        private IPuzzleView _viewModel;
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
        
        public void Init(IPuzzleView viewModel)
        {
            _viewModel = viewModel;
            _puzzleButtonSlotButton.Init(this);
        }

        public override void SetSelected(bool isSelect)
        {
            _isSelect = isSelect;
            Upgrade();
        }

        private void Upgrade()
        {
            if (_isSelect)
            {
                _image.sprite = _selectedSprite;
            }
            else
            {
                _image.sprite = _unelectedSprite;
            }
        }
    }
}