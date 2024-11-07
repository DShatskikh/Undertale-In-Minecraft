using System.Collections;
using System.Collections.Generic;
using RimuruDev;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PuzzleLeversView : UIPanelBase, IPuzzleView
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private PuzzleLeversArrow _arrow;

        [SerializeField]
        private Button _backButton;

        private PuzzleLevers _model;

        public override void Activate(bool isActive)
        {
            base.Activate(isActive);
            
            if (isActive)
            {
                var assetProvider = GameData.AssetProvider;
                
                for (int i = 0; i < _model.CurrentProgress.Count; i++)
                {
                    var slot = Instantiate(assetProvider.PuzzleLeverSlotPrefab, _container);
                    slot.Init(this);
                    _model.CurrentProgress[i].Changed += slot.SetOn;
                    slot.SetOn(_model.CurrentProgress[i].Value);
                    int rowIndex = 0;
                    int columnIndex = i;
                    _slots.Add(new Vector2(columnIndex, rowIndex), slot);
                }

                var buttonSlot = Instantiate(assetProvider.PuzzleButtonSlotPrefab, _container);
                buttonSlot.Init(this);
                _slots.Add(new Vector2(_model.CurrentProgress.Count, 0), buttonSlot);

                if (GameData.DeviceType == CurrentDeviceType.Mobile)
                {
                    _backButton.gameObject.SetActive(true);
                    _backButton.onClick.AddListener(Close);
                }
                
                _currentSlot.SetSelected(true);
                _arrow.SetCurrentSlot(_currentSlot.transform);
            }
            else
            {
                for (int i = 0; i < _slots.Count; i++)
                {
                    var slot = _slots[new Vector2(i, 0)];

                    if (slot is PuzzleLeverSlotView leverSlotView)
                        _model.CurrentProgress[i].Changed -= leverSlotView.SetOn;

                    Destroy(slot.gameObject);
                }

                _slots = new Dictionary<Vector2, BaseSlotController>();
                
                _backButton.onClick.RemoveListener(Close);
                GameData.CharacterController.enabled = true;
            }
        }

        private void Close()
        {
            Activate(false);
        }

        public void SetModel(PuzzleLevers model)
        {
            _model = model;
        }

        public override void OnSubmitDown()
        {
            
        }

        public override void OnSubmitUp()
        {
            if (_currentSlot is PuzzleButtonSlotView buttonSlotView)
            {
                if (_model.TryDecision())
                {
                    Close();
                }
                else
                {
                    GameData.EffectSoundPlayer.Play(GameData.AssetProvider.LeverSound);
                }
            }
            else if (_currentSlot is PuzzleLeverSlotView leverSlotView)
            {
                _model.CurrentProgress[(int)_currentIndex.x].Value = !_model.CurrentProgress[(int)_currentIndex.x].Value;
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.LeverSound);
            }
        }

        public override void OnCancel()
        {
            Close();
        }

        public override void OnSlotIndexChanged(Vector2 direction)
        {
            base.OnSlotIndexChanged(direction);
            _arrow.SetCurrentSlot(_currentSlot.transform);
        }

        public void SelectSlot(BaseSlotController slotViewModel)
        {
            if (_currentSlot == slotViewModel)
                return;
            
            _currentSlot.SetSelected(false);
            
            foreach (var slot in _slots)
            {
                if (slot.Value == slotViewModel)
                {
                    _currentIndex = slot.Key;
                    break;
                }
            }
            
            _currentSlot.SetSelected(true);
            _arrow.SetCurrentSlot(_currentSlot.transform);
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SelectSound);
        }
        
        private IEnumerator AwaitWrong()
        {
            yield return new WaitForSeconds(1);
        }
    }
}