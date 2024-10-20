using System;
using System.Collections;
using System.Linq;
using Cinemachine;
using MoreMountains.Feedbacks;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class GameplayStartup : MonoBehaviour
    {
        [FormerlySerializedAs("_character")] [SerializeField]
        private CharacterController characterController;
        
        [FormerlySerializedAs("_heart")] [SerializeField]
        private HeartController heartController;

        [SerializeField]
        private Battle _battle;

        [SerializeField]
        private DialogViewModel _dialog;

        [SerializeField]
        private MonologViewModel _monolog;
        
        [SerializeField]
        private GameObject _input;

        [SerializeField]
        private Button _useButton;

        [SerializeField]
        private Joystick _joystick;

        [SerializeField]
        private SelectViewModel _select;

        [SerializeField]
        private Transform _characterPoint, _enemyPoint;
        
        [SerializeField]
        private CinemachineConfiner2D _cinemachineConfiner;

        [SerializeField]
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        
        [SerializeField]
        private GameObject _introduction;
        
        [SerializeField]
        private LocationsManager _locationsManager;
        
        [SerializeField]
        private TMP_Text _saveText;
                
        [SerializeField]
        private Button _toMenuButton;

        [SerializeField]
        private GameObject _gameOver;
        
        [SerializeField]
        private CommandManager _commandManager;

        [FormerlySerializedAs("companionsManager")] [FormerlySerializedAs("_companionManager")] [SerializeField]
        private CompanionsManager _companionsManager;
        
        [SerializeField]
        private EndingsManager _endingsManager;

        [SerializeField]
        private SaverTimer _saverTimer;

        [SerializeField]
        private MMF_Player _impulseMMFPlayer;

        [SerializeField]
        private InputManager _inputManager;

        private void Awake()
        {
            GameData.CharacterController = characterController;
            GameData.HeartController = heartController;
            GameData.Battle = _battle;
            GameData.Dialog = _dialog;
            GameData.Monolog = _monolog;
            GameData.Select = _select;
            GameData.UseButton = _useButton;
            GameData.Joystick = _joystick;
            GameData.CharacterPoint = _characterPoint;
            GameData.EnemyPoint = _enemyPoint;
            GameData.CinemachineConfiner = _cinemachineConfiner;
            GameData.LocationsManager = _locationsManager;
            GameData.Introduction = _introduction;
            GameData.SaveText = _saveText;
            GameData.GameOver = _gameOver;
            GameData.CommandManager = _commandManager;
            GameData.CompanionsManager = _companionsManager;
            GameData.EndingsManager = _endingsManager;
            GameData.CinemachineVirtualCamera = _cinemachineVirtualCamera;
            GameData.SaverTimer = _saverTimer;
            GameData.InputManager = _inputManager;
            GameData.ImpulseMMFPlayer = _impulseMMFPlayer;
        }

        private void Start()
        {
            RegisterLuaFunctions();
            
            foreach (var saveLoad in FindObjectsByType<SaveLoadBase>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                saveLoad.Load();
            }

            YandexGame.savesData.IsNotIntroduction = true;

            if (!YandexGame.savesData.IsNotIntroduction)
            {
                _introduction.SetActive(true);
                YandexGame.savesData.IsNotFirstPlay = true;
            }
            else
            {
                _input.SetActive(true);
                GameData.Joystick.gameObject.SetActive(true);
                GameData.CharacterController.enabled = true;
                GameData.CharacterController.gameObject.SetActive(true);
                GameData.LocationsManager.SwitchLocation(YandexGame.savesData.LocationIndex, YandexGame.savesData.PointIndex);
                GameData.CharacterController.transform.position = GameData.Saver.LoadPosition();
            }

            StartCoroutine(Await());
        }

        private void OnDestroy()
        {
            Lua.UnregisterFunction(nameof(IsWin));
        }

        private void RegisterLuaFunctions()
        {
            Lua.RegisterFunction(nameof(IsWin), this, SymbolExtensions.GetMethodInfo(() => IsWin(string.Empty)));
        }

        private bool IsWin(string nameEnemy) => 
            (YandexGame.savesData.GetInt("IsWin" + nameEnemy) == 1);

        private IEnumerator Await()
        {
            yield return new WaitForSeconds(0.5f);
            GameData.IsCanStartBattle = true;
        }
    }
}