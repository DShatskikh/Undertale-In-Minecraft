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
        private CurrentDeviceType _testDeviceType = CurrentDeviceType.Mobile;
        
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

        [SerializeField]
        private AdsManager _adsManager;
        
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
            GameData.Mixer = _mixer;
            GameData.VolumeSlider = _volumeSlider;
            GameData.AdsManager = _adsManager;
            
            YandexGame.savesData.Health = YandexGame.savesData.MaxHealth;
            Application.targetFrameRate = 60;
            
            GameData.Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, YandexGame.savesData.Volume));

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