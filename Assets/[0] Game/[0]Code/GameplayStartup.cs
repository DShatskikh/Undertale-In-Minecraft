using System.Linq;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game
{
    public class GameplayStartup : MonoBehaviour
    {
        [SerializeField]
        private Character _character;
        
        [SerializeField]
        private Heart _heart;

        [SerializeField]
        private Battle _battle;

        [SerializeField]
        private Dialog _dialog;

        [SerializeField]
        private Monolog _monolog;
        
        [SerializeField]
        private GameObject _input;

        [SerializeField]
        private Button _useButton;

        [SerializeField]
        private Joystick _joystick;
        
        [SerializeField]
        private Select _select;

        [SerializeField]
        private GameObject _arena;
        
        [SerializeField]
        private Transform _characterPoint, _enemyPoint;
        
        [SerializeField]
        private CinemachineConfiner2D _cinemachineConfiner;

        [SerializeField]
        private GameObject _introduction;
        
        [SerializeField]
        private Locations _locations;
        
        [SerializeField]
        private TMP_Text _saveText;
                
        [SerializeField]
        private Button _toMenuButton;
        
        [SerializeField]
        private GameObject _inputCanvas;

        [SerializeField]
        private GameObject _gameOver;

        [SerializeField]
        private TimerBeforeAdsYG _timerBeforeAds;

        private void Awake()
        {
            GameData.Character = _character;
            GameData.Heart = _heart;
            GameData.Battle = _battle;
            GameData.Dialog = _dialog;
            GameData.Monolog = _monolog;
            GameData.Select = _select;
            GameData.UseButton = _useButton;
            GameData.Joystick = _joystick;
            GameData.Arena = _arena;
            GameData.CharacterPoint = _characterPoint;
            GameData.EnemyPoint = _enemyPoint;
            GameData.CinemachineConfiner = _cinemachineConfiner;
            GameData.Locations = _locations.Locations1;
            GameData.Introduction = _introduction;
            GameData.SaveText = _saveText;
            GameData.ToMenuButton = _toMenuButton;
            GameData.InputCanvas = _inputCanvas;
            GameData.GameOver = _gameOver;
            GameData.TimerBeforeAdsYG = _timerBeforeAds;
        }

        private void Start()
        {
            GameData.IsNotFirstPlay = true;
            GameData.Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, GameData.Volume));

            foreach (var saveLoad in FindObjectsByType<SaveLoadBase>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                saveLoad.Load();
            }
            
            if (!GameData.IsNotIntroduction)
            {
                _introduction.SetActive(true);
            }
            else
            {
                _input.SetActive(true);
                GameData.Joystick.gameObject.SetActive(true);
                GameData.TimerBeforeAdsYG.gameObject.SetActive(true);
                GameData.Character.enabled = true;
                GameData.Character.gameObject.SetActive(true);
                GameData.Character.transform.position = GameData.Saver.LoadPosition();
                GameData.Locations.ToArray()[GameData.LocationIndex].gameObject.SetActive(true);
                GameData.ToMenuButton.gameObject.SetActive(true);
                
                if (GameData.IsHat)
                    GameData.Character.HatPoint.Show();
                else
                    GameData.Character.HatPoint.Hide();
            }
        }
    }
}