using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MainMenu : MonoBehaviour
    {
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
        private GameObject _palesos;
        
        private void Awake()
        {
            _menu.SetActive(false);
        }

        private void Start()
        {
            if (!GameData.IsNotFirstPlay)
            {
                GameData.IsNotFirstPlay = true;
                GameData.Volume = 1;
                PlayerPrefs.SetFloat("Volume", GameData.Volume);
                SceneManager.LoadScene(1);
            }
            else
            {
                _menu.SetActive(true);

                if (!GameData.IsNotIntroduction)
                {
                    _notGamePreset.SetActive(true);
                    _continuePreset.SetActive(false);
                }
                else
                {
                    _notGamePreset.SetActive(false);
                    _continuePreset.SetActive(true);
                }

                if (GameData.IsGoodEnd && GameData.IsBadEnd) 
                    _guide.SetActive(true);

                if (GameData.IsGoldKey)
                {
                    _strangeEnd.SetActive(true);
                    _fullReset.SetActive(true);
                }
                
                if (GameData.IsHat) 
                    _cake.SetActive(true);
                
                if (GameData.IsCheat) 
                    _mask.SetActive(true);
                
                if (GameData.IsBadEnd) 
                    _badEnd.SetActive(true);
                
                if (GameData.IsGoodEnd) 
                    _goodEnd.SetActive(true);
                
                if (GameData.Palesos != 0)
                    _palesos.SetActive(true);
            }
        }
    }
}