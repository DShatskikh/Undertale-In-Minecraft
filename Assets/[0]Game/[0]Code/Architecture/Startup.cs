using RimuruDev;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using YG;

namespace Game
{
    public class Startup : MonoBehaviour
    {
        [SerializeField]
        private bool _isNotLoad;
        
        [Header("Переменные")]
        [SerializeField]
        private CurrentDeviceType _testDeviceType = CurrentDeviceType.Mobile;
        
        [FormerlySerializedAs("_soundMixer")]
        [Header("Ссылки")]
        [SerializeField]
        private AudioMixer _audioMixer;

        [SerializeField]
        private EffectSoundPlayer _effectSoundPlayer;

        [SerializeField]
        private MusicPlayer _musicPlayer;
        
        [SerializeField]
        private DeviceTypeDetector _deviceTypeDetector;

        [SerializeField]
        private AssetProvider _assetProvider;

        [SerializeField]
        private AdsManager _adsManager;

        [SerializeField]
        private PlayerInput _playerInput;
        
        private PurchaseManager _purchaseManager;
        
        private void Awake()
        {
            if (FindObjectsByType<Startup>(FindObjectsSortMode.None).Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            _purchaseManager = new PurchaseManager();
            GameData.DeviceType = _deviceTypeDetector.CurrentDeviceType;
            
            YandexGame.GetDataEvent += () => GameData.IsLoad = true;
            GameData.Saver = new Saver();
            GameData.PlayerInput = _playerInput;
            
#if UNITY_EDITOR
            GameData.DeviceType = _testDeviceType;
#endif
            
            GameData.Saver.Load();
        }

        private void Start()
        {
            GameData.Startup = this;
            GameData.AssetProvider = _assetProvider;
            GameData.EffectSoundPlayer = _effectSoundPlayer;
            GameData.MusicPlayer = _musicPlayer;
            GameData.AudioMixer = _audioMixer;
            GameData.AdsManager = _adsManager;
            
            YandexGame.savesData.Health = YandexGame.savesData.MaxHealth;
            Application.targetFrameRate = 60;

#if UNITY_EDITOR
            if (_isNotLoad)
                return;
#endif
        }

        private void OnDestroy()
        {
            YandexGame.GetDataEvent -= () => GameData.IsLoad = true;
        }
    }
}