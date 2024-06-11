using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class Saver : MonoBehaviour
    {
        [ShowInInspector]
        private List<SaveKeyIntPair> _dataInt = new();
        
        [SerializeField]
        private List<SaveKeyBoolPair> _dataBool = new();
        
        [ShowInInspector]
        private Dictionary<string, string> _dataString = new();

        [ContextMenu("LoadKeysInInspector")]
        public void LoadKeysInInspector()
        {
            _dataInt = new List<SaveKeyIntPair>();
            
            foreach (var saveKeyInt in Resources.LoadAll<SaveKeyInt>("Data/SaveKeys/Int"))
                _dataInt.Add(new SaveKeyIntPair(saveKeyInt, LoadKey(saveKeyInt)));

            _dataBool = new List<SaveKeyBoolPair>();
            
            foreach (var saveKeyBool in Resources.LoadAll<SaveKeyBool>("Data/SaveKeys/Bool"))
                _dataBool.Add(new SaveKeyBoolPair(saveKeyBool, LoadKey(saveKeyBool)));
            
            //foreach (var saveKeyString in Resources.LoadAll<SaveKeyString>("Data/SaveKeys/String"))
            //    _dataString.Add(saveKeyString.name, PlayerPrefs.GetString(saveKeyString.name, saveKeyString.DefaultValue));
        }

        public void Load()
        {
            GameData.Version = PlayerPrefs.GetString("Version");
            GameData.Palesos = PlayerPrefs.GetInt("Palesos");
            GameData.LocationIndex = PlayerPrefs.GetInt("LocationIndex");
            GameData.Volume = PlayerPrefs.GetFloat("Volume", 1);
            Debug.Log("Игра загруженна");
        }

        public void SaveAll()
        {
            SavePlayerPosition();

            PlayerPrefs.SetInt("Palesos", GameData.Palesos);
            PlayerPrefs.SetInt("LocationIndex", GameData.LocationIndex);
            PlayerPrefs.SetFloat("Volume", GameData.Volume);

            PlayerPrefs.SetString("Version", Application.version);
            
            Debug.Log("Игра сохранена");
        }

        public void Save(SaveKeyBool key, bool value) => 
            PlayerPrefs.SetInt(key.name, value ? 1 : 0);

        public void Save(SaveKeyInt key, int value) => 
            PlayerPrefs.SetInt(key.name, value);
        
        private void SavePosition(Vector2 value)
        {
            PlayerPrefs.SetFloat("PositionX", value.x);
            PlayerPrefs.SetFloat("PositionY", value.y);
        }
        
        public Vector2 LoadPosition()
        {
            return new Vector2(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"));
        }

        public bool LoadKey(SaveKeyBool key) => 
            GetBool(key.name, key.DefaultValue);
        
        public int LoadKey(SaveKeyInt key) => 
            PlayerPrefs.GetInt(key.name, key.DefaultValue);
        
        public string LoadKey(SaveKeyString key) => 
            PlayerPrefs.GetString(key.name);

        private bool GetBool(string key, bool defaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }

        public void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public void Reset()
        {
            PlayerPrefs.SetInt("MaxHealth", 20);
            //PlayerPrefs.SetInt("Palesos", 0);
            PlayerPrefs.SetInt("LocationIndex", 0);

            SetBool("IsHat", false);
            SetBool("IsNotIntroduction", false);
            SetBool("IsCheat", false);
            SetBool("IsPrisonKey", false);
            //SetBool("IsGoldKey", false);
            SetBool("IsDeveloperKey", false);
            SetBool("IsSpeakHerobrine", false);
            SetBool("IsCapturedWorld", false);
            SetBool("IsNotCapturedWorld", false);
        }

        public void SavePlayerPosition()
        {
            SavePosition(GameData.Character.transform.position);
        }
    }
}