using UnityEngine;

namespace Game
{
    public class KeySlotViewModel : BaseSlotController
    {
        [SerializeField]
        private string _keyHash;
        
        private KeySlotView _view;
        private bool _isSelect;

        private void Awake()
        {
            _view = GetComponent<KeySlotView>();
        }

        private void Start()
        {
            _view.Init(this, _keyHash);
        }

        private void Update()
        {
            if (!_isSelect)
                return;

            if (Input.GetButtonDown("Submit"))
            {

            }
        }

        public override void SetSelected(bool isSelect)
        {
            _isSelect = isSelect;
            _view.SetSelect(isSelect);
        }
    }
}