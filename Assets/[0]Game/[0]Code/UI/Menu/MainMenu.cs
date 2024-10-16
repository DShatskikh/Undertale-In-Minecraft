using System;
using System.Collections.Generic;
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
        private MenuSlotConfig _startGameConfig, _continueConfig, _resetConfig;

        [SerializeField]
        private MenuSlotViewModel _fullResetSlot;

        [SerializeField]
        private ResetScreen _resetScreen;

        private void Start()
        {
            var assetProvider = GameData.AssetProvider;
            var slotsData = new List<MenuSlotConfig>();

            if (!YandexGame.savesData.IsNotFirstPlay)
                slotsData.Add(_startGameConfig);
            else
            {
                slotsData.Add(_continueConfig);
                slotsData.Add(_resetConfig);
            }
            
            foreach (var config in assetProvider.MenuSlotConfigs) 
                slotsData.Add(config);

            for (int i = 0; i < slotsData.Count; i++)
            {
                var model = slotsData[i];
                var slot = Instantiate(assetProvider.MenuSlotPrefab, _container);
                slot.Model = model;
                int rowIndex = slotsData.Count - i - 1;
                int columnIndex = 0;
                slot.SetSelected(false);
                _slots.Add(new Vector2(columnIndex, rowIndex), slot);
            }
            
            if (YandexGame.savesData.IsGoldKey) 
                _slots.Add(new Vector2(0, slotsData.Count), _fullResetSlot);

            foreach (var slot in _slots) 
                ((MenuSlotViewModel)slot.Value).Init(this);

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
                    GameData.Saver.Reset();
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