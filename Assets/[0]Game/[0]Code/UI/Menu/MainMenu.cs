using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
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
        
        [SerializeField]
        private GameObject _guide;
        
        [SerializeField]
        private GameObject _cake;
        
        [SerializeField]
        private GameObject _mask;
        
        [SerializeField]
        private GameObject _badEnd;
        
        [SerializeField]
        private GameObject _goodEnd;
        
        [SerializeField]
        private GameObject _strangeEnd;
        
        [SerializeField]
        private GameObject _palesosEnd;
                
        [SerializeField]
        private GameObject _winDesiccant;
        
        [SerializeField]
        private GameObject _palesos;

        /*private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                YandexGame.ResetSaveProgress();
        }*/

        private IEnumerator Start()
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
            
            _currentIndex = new Vector2(0, _slots.Count - 1);
            _currentSlot.SetSelected(true);
            
            Activate(true);
            yield return LocalizationSettings.InitializationOperation;

            if (YandexGame.lang == "en")
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
                yield return LocalizationSettings.InitializationOperation;  
            }

            yield return new WaitUntil(() => GameData.IsLoad);
            
            if (!YandexGame.savesData.IsNotFirstPlay)
            {
                print("IsNotFirstPlay False");
                YandexGame.savesData.IsNotFirstPlay = true;
                YandexGame.SaveProgress();
                //yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
                SceneManager.LoadScene(1);
            }
            else
            {
                if (YandexGame.savesData.IsGoodEnd && YandexGame.savesData.IsBadEnd) 
                    _guide.SetActive(true);

                if (YandexGame.savesData.IsStrangeEnd) 
                    _strangeEnd.SetActive(true);

                if (YandexGame.savesData.IsPalesosEnd) 
                    _palesosEnd.SetActive(true);
                
                if (YandexGame.savesData.IsCake) 
                    _cake.SetActive(true);
                
                if (YandexGame.savesData.IsCheat) 
                    _mask.SetActive(true);
                
                if (YandexGame.savesData.IsBadEnd) 
                    _badEnd.SetActive(true);
                
                if (YandexGame.savesData.IsGoodEnd) 
                    _goodEnd.SetActive(true);
                
                if (YandexGame.savesData.Palesos != 0)
                    _palesos.SetActive(true);
                
                //if (YandexGame.savesData.IsGoodEnd) 
                //    _winDesiccant.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            foreach (var slot in _slots) 
                Destroy(slot.Value.gameObject);

            _slots = new Dictionary<Vector2, BaseSlotController>();
        }

        public override void Activate(bool isActive)
        {
            base.Activate(isActive);

            if (isActive)
            {

            }
            else
            {
                
            }
        }

        public override void OnSubmit()
        {
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
                    _resetScreen.Activate(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void OnCancel()
        {
            
        }
    }
}