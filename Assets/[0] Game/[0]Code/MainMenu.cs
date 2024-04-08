using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _menu;
        
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
            }
        }
    }
}