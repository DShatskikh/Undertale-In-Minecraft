using RimuruDev;
using UnityEngine;
using UnityEngine.Audio;
using YG;

namespace Game
{
    public class Startup : MonoBehaviour
    {
        [SerializeField]
        private bool _isNotLoad;
        
        [Header("Переменные")] 
        [SerializeField]
        private bool _isCheat;
        
        [SerializeField]
        private bool _isPrisonKey;

        [SerializeField]
        private bool _isGoldKey;
        
        [SerializeField]
        private bool _isDeveloperKey;
        
        [SerializeField]
        private bool _isSpeakHerobrine;
        
        [SerializeField]
        private int _startHealth;
        
        [SerializeField]
        private bool _isHat;
        
        [SerializeField]
        private bool _isCapturedWorld;
        
        [SerializeField]
        private bool _isNotCapturedWorld;

        [SerializeField]
        private int _palesos;

        [SerializeField] 
        private bool _isGoodEnd;
        
        [SerializeField]
        private bool _isBadEnd;
        
        [SerializeField]
        private bool _isStrangeEnd;

        [SerializeField]
        private bool _isNotIntroduction;

        [SerializeField]
        private CurrentDeviceType _testDeviceType = CurrentDeviceType.WebMobile;
        
        [Header("Ссылки")]
        [SerializeField]
        private AudioMixerGroup _mixer;
        
        [SerializeField]
        private EffectSoundPlayer _effectSoundPlayer;

        [SerializeField]
        private MusicPlayer _musicPlayer;
        
        [SerializeField]
        private DeviceTypeDetector _deviceTypeDetector;

        [SerializeField]
        private VolumeSlider _volumeSlider;

        [SerializeField]
        private AssetProvider _assetProvider;
        
        private void Awake()
        {
            if (FindObjectsOfType<Startup>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            GameData.DeviceType = _deviceTypeDetector.CurrentDeviceType;
            
            YandexGame.GetDataEvent += () => GameData.IsLoad = true;
            GameData.Saver = new Saver();
            
#if UNITY_EDITOR
            GameData.DeviceType = _testDeviceType;
            
            if (_isNotLoad)
            {
                YandexGame.savesData.MaxHealth = _startHealth;
                YandexGame.savesData.IsCheat = _isCheat;
                YandexGame.savesData.IsPrisonKey = _isPrisonKey;
                YandexGame.savesData.IsGoldKey = _isGoldKey;
                YandexGame.savesData.IsDeveloperKey = _isDeveloperKey;
                YandexGame.savesData.IsSpeakHerobrine = _isSpeakHerobrine;
                YandexGame.savesData.IsCapturedWorld = _isCapturedWorld;
                YandexGame.savesData.IsNotCapturedWorld = _isNotCapturedWorld;
                YandexGame.savesData.IsHat = _isHat;
                YandexGame.savesData.Palesos = _palesos;
                YandexGame.savesData.IsGoodEnd = _isGoodEnd;
                YandexGame.savesData.IsBadEnd = _isBadEnd;
                YandexGame.savesData.IsStrangeEnd = _isStrangeEnd;
                YandexGame.savesData.IsNotIntroduction = _isNotIntroduction;
                YandexGame.savesData.Volume = 1f;

                return;
            }
#endif
            
            GameData.Saver.Load();
        }

        private void Start()
        {
            GameData.Startup = this;
            GameData.AssetProvider = _assetProvider;
            GameData.EffectSoundPlayer = _effectSoundPlayer;
            GameData.MusicPlayer = _musicPlayer;
            GameData.Mixer = _mixer;
            GameData.VolumeSlider = _volumeSlider;
            
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