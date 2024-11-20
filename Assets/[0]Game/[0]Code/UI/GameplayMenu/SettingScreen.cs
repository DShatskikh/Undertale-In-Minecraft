using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using RimuruDev;
using UnityEngine;
using YG;

namespace Game
{
    public class SettingScreen : MenuPanelBase
    {
        [SerializeField]
        private MMF_Player _selectPlayer;
        
        [SerializeField]
        private MenuPanelBase _menu;
        
        [SerializeField]
        private bool _isExitUp = true;

        public override void Activate(bool isActive)
        {
            base.Activate(isActive);

            if (isActive)
            {
                _selectPlayer.PlayFeedbacks();

                _slots = new Dictionary<Vector2, BaseSlotController>();
                var slotsAll = GetComponentsInChildren<BaseSlotController>();
                var slots = new List<BaseSlotController>();

                for (int i = 0; i < slotsAll.Length; i++)
                {
                    bool isSkip = false;
                    
                    if (GameData.DeviceType == CurrentDeviceType.Mobile)
                    {
                        switch (slotsAll[i])
                        {
                            case HintSlotViewModel hintSlot:
                                isSkip = true;
                                break;
                            case ReassigningKeysSlotViewModel reassigningKeysSlot:
                                isSkip = true;
                                break;
                        } 
                    }

                    if (isSkip)
                    {
                        slotsAll[i].gameObject.SetActive(false);
                        continue;
                    }

                    slots.Add(slotsAll[i]);
                }

                for (int i = 0; i < slots.Count; i++)
                {
                    _slots.Add(new Vector2(0, slots.Count - i - 1), slots[i]);
                    slots[i].SetSelected(false);
                    
                    switch (slots[i])
                    {
                        case VolumeSlotViewModel volumeSlot:
                            volumeSlot.Init(this);
                            break;
                        case LanguageSlotViewModel languageSlot:
                            languageSlot.Init(this);
#if PLATFORM_WEBGL
                  languageSlot.gameObject.SetActive(false);       
#endif
                            
                            break;
                        case HintSlotViewModel hintSlot:
                            hintSlot.Init(this);
                            
#if PLATFORM_WEBGL
                  hintSlot.gameObject.SetActive(false);       
#endif
                            break;
                        case ReassigningKeysSlotViewModel reassigningKeysSlot:
                            reassigningKeysSlot.Init(this);
                            
#if PLATFORM_WEBGL
                  reassigningKeysSlot.gameObject.SetActive(false);       
#endif
                            break;
                    }
                }

                _currentIndex = new Vector2(0, _slots.Count - 1);
            }
            else
            {
                _slots = new Dictionary<Vector2, BaseSlotController>();
            }
        }

        public void SelectZero()
        {
            UnSelect();
            _currentIndex = new Vector2(0, _slots.Count - 1);
            Select();
        }

        public override void Select()
        {
            StartCoroutine(AwaitSelect());
        }

        private IEnumerator AwaitSelect()
        {
            yield return null;
            base.Select();
            
            if (_slots.TryGetValue(_currentIndex, out var slot))
                _currentSlot.SetSelected(true);
            
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SelectSound);
        }

        public override void UnSelect()
        {
            base.UnSelect();
            _currentSlot.SetSelected(false);
        }

        public override void OnSubmitDown() { }

        public override void OnSubmitUp()
        {
            if (!_isSelect)
                return;
            
            print(_isSelect);

            switch (_currentSlot)
            {
                case LanguageSlotViewModel languageSlot:
                    languageSlot.Click();
                    break;
                case HintSlotViewModel hintSlot:
                    GameData.SettingStorage.IsShowHint.Value = !GameData.SettingStorage.IsShowHint.Value;
                    GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
                    break;
                case ReassigningKeysSlotViewModel reassigningKeysSlot:
                    reassigningKeysSlot.Click();
                    GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
                    break;
            }
        }

        public override void OnCancel()
        {
            UnSelect();
            _menu.Select();

            if (!_isExitUp)
            {
                Activate(false);
                _menu.Activate(true); 
            }
        }

        public override void SelectSlot(BaseSlotController slotViewModel)
        {
            base.SelectSlot(slotViewModel);
            
            if (_menu is GameplayMenu gameplayMenu)
                gameplayMenu.UnSelect();
            
            Select();
        }
        
        public override void OnSlotIndexChanged(Vector2 direction)
        {
            if (!_isSelect)
                return;
            
            if (direction is { y: 1, x: 0 } && _currentIndex.y == _slots.Count - 1)
            {
                if (_isExitUp)
                {
                    UnSelect();
                    _menu.Select();
                }
            }
            
            base.OnSlotIndexChanged(direction);
        }
    }
}