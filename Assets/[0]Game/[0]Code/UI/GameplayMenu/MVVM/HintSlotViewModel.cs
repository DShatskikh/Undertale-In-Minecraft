using UnityEngine;

namespace Game
{
    public class HintSlotViewModel : BaseSlotController
    {
        private HintSlotView _view;
        private bool _isSelect;

        public readonly ReactiveProperty<bool> IsToggle = new();
        
        private void Awake()
        {
            _view = GetComponent<HintSlotView>();
        }
        
        private void Start()
        {
            _view.Init(this);
        }

        private void Update()
        {
            if (!_isSelect)
                return;

            if (Input.GetButtonDown("Submit"))
            {
                IsToggle.Value = !IsToggle.Value;
            }
        }

        public override void SetSelected(bool isSelect)
        {
            _isSelect = isSelect;
            _view.SetSelect(isSelect);
        }
    }
}