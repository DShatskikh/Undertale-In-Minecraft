using System;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Game
{
    public class MainMenu : MenuPanelBase
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private SettingScreen _settingScreen;
        
        [SerializeField]
        private MenuSlotConfig _startGameConfig, _continueConfig, _resetConfig, _exitConfig;

        [SerializeField]
        private MenuSlotViewModel _fullResetSlot;

        [SerializeField]
        private MenuSlotViewModel _donationSlot;
        
        [SerializeField]
        private ResetScreen _resetScreen;

        [SerializeField]
        private DonationScreen _donationScreen;
        
        private void Start()
        {
            var assetProvider = GameData.AssetProvider;
            var slotsData = new List<MenuSlotConfig>();

            if (Lua.IsTrue("Variable[\"IsNewGame\"] == true"))
                slotsData.Add(_startGameConfig);
            else
            {
                slotsData.Add(_continueConfig);
                slotsData.Add(_resetConfig);
            }

#if !UNITY_WEBGL
            slotsData.Add(_exitConfig);            
#endif
            
            slotsData.AddRange(assetProvider.MenuSlotConfigs);

            foreach (var model in slotsData)
            {
                var slot = Instantiate(assetProvider.MenuSlotPrefab, _container);
                slot.Model = model;
                var rowIndex = _slots.Count;
                var columnIndex = 0;
                slot.SetSelected(false);
                _slots.Add(new Vector2(columnIndex, rowIndex), slot);
            }

            if (YandexGame.savesData.IsGoldKey)
            {
                _slots.Add(new Vector2(0, slotsData.Count), _fullResetSlot);
                _fullResetSlot.gameObject.SetActive(true);
            }

            if (Lua.IsTrue("IsCompletedAllEnding() == true"))
            {
                _slots.Add(new Vector2(0, _slots.Count), _donationSlot);
                _donationSlot.gameObject.SetActive(true);
                _donationSlot.SetSelected(false);
            }

            foreach (var slot in _slots) 
                ((MenuSlotViewModel)slot.Value).Init(this);

            var copySlots = new Dictionary<Vector2, BaseSlotController>();
            
            for (int i = 0; i < _slots.Count; i++)
                copySlots.Add(new Vector2(0, i), _slots[new Vector2(0, i)]);
            
            for (int i = 0; i < _slots.Count; i++)
                _slots[new Vector2(0, i)] = copySlots[new Vector2(0, _slots.Count - i - 1)];

            _currentIndex = new Vector2(0, _slots.Count - 1);
            _currentSlot.SetSelected(true);
            
            Activate(true);
        }

        private void OnDestroy()
        {
            foreach (var slot in _slots) 
                Destroy(slot.Value.gameObject);

            _slots = new Dictionary<Vector2, BaseSlotController>();
        }

        public override void Activate(bool isActive)
        {
            if (isActive)
                Select();
            else
                UnSelect();

            base.Activate(isActive);
        }

        public override void OnSubmitDown()
        {
            _currentSlot.SubmitDown();
        }

        public override void OnSubmitUp()
        {
            base.OnSubmitUp();
            _currentSlot.SubmitUp();
            
            switch (((MenuSlotViewModel)_currentSlot).Model.MenuSlotType)
            {
                case MenuSlotType.StartGame:
                    SceneManager.LoadScene(1);
                    break;
                case MenuSlotType.Continue:
                    SceneManager.LoadScene(1);
                    break;
                case MenuSlotType.Setting:
                    _settingScreen.Activate(true);
                    _settingScreen.Select();
                    Activate(false);
                    break;
                case MenuSlotType.Exit:
                    Application.Quit();
                    break;
                case MenuSlotType.FullReset:
                    YandexGame.savesData.FullReset();
                    break;
                case MenuSlotType.Reset:
                    Activate(false);
                    _resetScreen.gameObject.SetActive(true);
                    break;
                case MenuSlotType.Donation:
                    _donationScreen.Activate(true);
                    Activate(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
        }

        public override void SelectSlot(BaseSlotController slotViewModel)
        {
            base.SelectSlot(slotViewModel);
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SelectSound);
        }

        public override void OnCancel()
        {
            
        }
    }
}