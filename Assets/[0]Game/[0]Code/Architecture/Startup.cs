using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
    public class Startup : MonoBehaviour
    {
        [Header("Переменные")] 
        [SerializeField]
        private bool _isNotLoad;

        [SerializeField]
        private List<SaveKeyBoolPair> _dataBool = new();
        
        [SerializeField]
        private List<SaveKeyIntPair> _dataInt = new();
        
        [SerializeField]
        private int _startHealth;
        
        [SerializeField]
        private int _palesos;

        [Header("Ссылки")]
        [SerializeField]
        private AudioMixerGroup _mixer;
        
        [SerializeField]
        private AudioSource _effectAudioSource, _musicAudioSource, _textAudioSource;

        [SerializeField]
        private AudioClip _clickSound;

        [SerializeField]
        private Saver _saver;

        private void Awake()
        {
            if (FindObjectsOfType<Startup>().Length > 1)
                Destroy(gameObject);
            else
                DontDestroyOnLoad(gameObject);
            
            GameData.Saver = _saver;
            
#if UNITY_EDITOR
            if (_isNotLoad)
            {
                foreach (var saveKey in _dataBool) 
                    _saver.Save(saveKey.Key, saveKey.Value);

                foreach (var saveKey in _dataInt) 
                    _saver.Save(saveKey.Key, saveKey.Value);
                
                GameData.MaxHealth = _startHealth;
                GameData.Palesos = _palesos;
                GameData.Volume = 1f;

                return;
            }
#endif
            
            GameData.Volume = 1f;
            GameData.Saver.LoadKeysInInspector();
            GameData.Saver.Load();
        }

        private void Start()
        {
            GameData.Startup = this;
            GameData.EffectAudioSource = _effectAudioSource;
            GameData.MusicAudioSource = _musicAudioSource;
            GameData.Mixer = _mixer;
            GameData.TextAudioSource = _textAudioSource;
            GameData.ClickSound = _clickSound;
            
            GameData.Health = GameData.MaxHealth;
            Application.targetFrameRate = 60;
            
#if UNITY_EDITOR
            if (_isNotLoad)
            {
                SaveCustomValues();
                return;
            }
#endif
        }

        [ContextMenu("LoadKeys")]
        public void LoadKeys()
        {
            _dataBool = new List<SaveKeyBoolPair>();
            
            //foreach (var saveKeyInt in Resources.LoadAll<SaveKeyInt>("Data/SaveKeys/Int"))
                //_dataInt.Add(saveKeyInt.name, PlayerPrefs.GetInt(saveKeyInt.name, saveKeyInt.DefaultValue));
            
            foreach (var saveKeyBool in Resources.LoadAll<SaveKeyBool>("Data/SaveKeys/Bool"))
                _dataBool.Add(new SaveKeyBoolPair(saveKeyBool, saveKeyBool.DefaultValue));
            
            //foreach (var saveKeyString in Resources.LoadAll<SaveKeyString>("Data/SaveKeys/String"))
                //_dataString.Add(saveKeyString.name, PlayerPrefs.GetString(saveKeyString.name, saveKeyString.DefaultValue));
                
                print("Load Keys Successful");
        }

        [ContextMenu("SaveCustomValues")]
        public void SaveCustomValues()
        {
            foreach (var data in _dataBool) 
                _saver.Save(data.Key, data.Value);
            
            print("Save Custom Data Successful");
        }
    }
}