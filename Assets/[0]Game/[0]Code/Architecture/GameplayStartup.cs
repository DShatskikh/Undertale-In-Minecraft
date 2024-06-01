using System.Linq;
using Cinemachine;
using UnityEngine;

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
        private Select _select;

        [SerializeField]
        private GameObject _arena;
        
        [SerializeField]
        private Transform _characterPoint, _enemyPoint;
        
        [SerializeField]
        private GameObject _introduction;
        
        [SerializeField]
        private Locations _locations;

        [SerializeField]
        private GameObject _gameOver;

        [SerializeField]
        private GameObject _arrow;

        [SerializeField]
        private CinemachineConfiner2D _cinemachineConfiner;

        [SerializeField]
        private GameMenu _gameMenu;

        [SerializeField]
        private SubmitUpdater _submitUpdater;
        
        [SerializeField]
        private CancelUpdater _cancelUpdater;
        
        [SerializeField]
        private OpenMenuUpdater _openMenuUpdater;
        
        [Header("Keys")]
        [SerializeField]
        private SaveKeyInt _moneyKey;
        
        [SerializeField]
        private SaveKeyBool _isIntroductionKey;
        
        [SerializeField]
        private SaveKeyBool _isHatKey;

        [SerializeField]
        private SaveKeyBool _isCheatKey;

        [SerializeField]
        private MoneyLabel _moneyLabel;
        
        private void Awake()
        {
            GameData.CoroutineRunner = this;
            GameData.Character = _character;
            GameData.Heart = _heart;
            GameData.Battle = _battle;
            GameData.Dialog = _dialog;
            GameData.Monolog = _monolog;
            GameData.Select = _select;
            GameData.Arena = _arena;
            GameData.CharacterPoint = _characterPoint;
            GameData.EnemyPoint = _enemyPoint;
            GameData.Locations = _locations.Locations1;
            GameData.GameOver = _gameOver;
            GameData.Arrow = _arrow;
            GameData.CinemachineConfiner = _cinemachineConfiner;
            GameData.GameMenu = _gameMenu;
            GameData.SubmitUpdater = _submitUpdater;
            GameData.CancelUpdater = _cancelUpdater;
            GameData.OpenMenuUpdater = _openMenuUpdater;
            GameData.MoneyKey = _moneyKey;
            GameData.MoneyLabel = _moneyLabel;
        }

        private void Start()
        {
            GameData.Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, GameData.Volume));

            foreach (var saveLoad in FindObjectsByType<SaveLoadBase>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                saveLoad.Load();
            }
            
            if (GameData.Saver.LoadKey(_isIntroductionKey))
            {
                _introduction.SetActive(true);
            }
            else
            {
                GameData.Character.enabled = true;
                GameData.Character.gameObject.SetActive(true);
                GameData.Character.transform.position = GameData.Saver.LoadPosition();
                GameData.Locations.ToArray()[GameData.LocationIndex].gameObject.SetActive(true);

                if (GameData.Saver.LoadKey(_isHatKey))
                    GameData.Character.HatPoint.Show();
                
                if (GameData.Saver.LoadKey(_isCheatKey))
                    GameData.Character.HackerMask.Show();
            }
            
            GameData.Saver.Save(GameData.MoneyKey, 999);
        }
    }
}