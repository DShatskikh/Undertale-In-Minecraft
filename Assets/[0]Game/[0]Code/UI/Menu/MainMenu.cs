using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using YG;

namespace Game
{
    public class MainMenu : UIPanelBase
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private MenuSlotConfig _startGameConfig, _continueConfig;

        [SerializeField]
        private MenuSlotViewModel _fullResetSlot;
        
        [SerializeField]
        private GameObject _menu;

        [SerializeField]
        private GameObject _notGamePreset;
        
        [SerializeField]
        private GameObject _continuePreset;
        
        [SerializeField]
        private GameObject _fullReset;

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

        private void Awake()
        {
            _menu.SetActive(false);
        }

        /*private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                YandexGame.ResetSaveProgress();
        }*/

        private IEnumerator Start()
        {
            var assetProvider = GameData.AssetProvider;
            var slotsData = assetProvider.MenuSlotConfigs;

            if (!YandexGame.savesData.IsNotFirstPlay)
            {
                slotsData[0] = _startGameConfig;
            }
            else
            {
                slotsData[0] = _continueConfig;
            }

            for (int i = 0; i < slotsData.Length; i++)
            {
                var model = slotsData[i];
                var slot = Instantiate(assetProvider.MenuSlotPrefab, _container);
                slot.Model = model;
                int rowIndex = slotsData.Length - i - 1;
                int columnIndex = 0;
                slot.SetSelected(false);
                _slots.Add(new Vector2(columnIndex, rowIndex), slot);
            }
            
            if (YandexGame.savesData.IsGoldKey) 
                _slots.Add(new Vector2(0, slotsData.Length), _fullResetSlot);
            
            _currentIndex = new Vector2(0, _slots.Count - 1);
            _slots[_currentIndex].SetSelected(true);
            
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
                _menu.SetActive(true);

                if (!YandexGame.savesData.IsNotIntroduction)
                {
                    _notGamePreset.SetActive(true);
                    _continuePreset.SetActive(false);
                }
                else
                {
                    _notGamePreset.SetActive(false);
                    _continuePreset.SetActive(true);
                }

                if (YandexGame.savesData.IsGoodEnd && YandexGame.savesData.IsBadEnd) 
                    _guide.SetActive(true);

                if (YandexGame.savesData.IsStrangeEnd) 
                    _strangeEnd.SetActive(true);

                if (YandexGame.savesData.IsPalesosEnd) 
                    _palesosEnd.SetActive(true);

                if (YandexGame.savesData.IsGoldKey) 
                    _fullReset.SetActive(true);

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
                    //Setting
                    break;
                case MenuSlotType.Exit:
                    Application.Quit();
                    break;
                case MenuSlotType.Reset:
                    YandexGame.savesData.FullReset();
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