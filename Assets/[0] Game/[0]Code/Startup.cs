using UnityEngine;
using UnityEngine.Audio;

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

        [Header("Ссылки")]
        [SerializeField]
        private AudioMixerGroup _mixer;
        
        [SerializeField]
        private AudioSource _effectAudioSource, _musicAudioSource;

        private void Awake()
        {
            if (FindObjectsOfType<Startup>().Length > 1)
                Destroy(gameObject);
            else
                DontDestroyOnLoad(gameObject);
            
            GameData.Saver = new Saver();
            
#if UNITY_EDITOR
            if (_isNotLoad)
            {
                GameData.MaxHealth = _startHealth;
                GameData.IsCheat = _isCheat;
                GameData.IsPrisonKey = _isPrisonKey;
                GameData.IsGoldKey = _isGoldKey;
                GameData.IsDeveloperKey = _isDeveloperKey;
                GameData.IsSpeakHerobrine = _isSpeakHerobrine;
                GameData.IsCapturedWorld = _isCapturedWorld;
                GameData.IsNotCapturedWorld = _isNotCapturedWorld;
                GameData.IsHat = _isHat;
                GameData.Palesos = _palesos;
                GameData.IsGoodEnd = _isGoodEnd;
                GameData.IsBadEnd = _isBadEnd;
                GameData.IsStrangeEnd = _isStrangeEnd;
                GameData.IsNotIntroduction = _isNotIntroduction;
                GameData.Volume = 1f;

                return;
            }
#endif
            
            GameData.Saver.Load();
        }

        private void Start()
        {
            GameData.Startup = this;
            GameData.EffectAudioSource = _effectAudioSource;
            GameData.MusicAudioSource = _musicAudioSource;
            GameData.Mixer = _mixer;
            
            GameData.Health = GameData.MaxHealth;
            Application.targetFrameRate = 60;
            
#if UNITY_EDITOR
            if (_isNotLoad)
                return;
#endif
        }

    }
}