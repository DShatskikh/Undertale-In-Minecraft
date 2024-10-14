using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using YG;

namespace Game
{
    public class MainMenuBackground : MonoBehaviour
    {
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
        
        private IEnumerator Start()
        {
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
                //if (YandexGame.savesData.IsGoodEnd && YandexGame.savesData.IsBadEnd) 
                    //_guide.SetActive(true);

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
    }
}