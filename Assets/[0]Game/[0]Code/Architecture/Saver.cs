using System;
using System.Collections.Generic;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Random = UnityEngine.Random;

namespace Game
{
    public class Saver
    {
        public bool IsSave = true;
        private readonly SaveSystem _saveSystem;

        private Dictionary<string, bool> _boolPair = new Dictionary<string, bool>();

        private string[] _boolHash = new[]
        {
            "IsBavGoodEnding",
            "IsBavBadEnding",
            "IsBavStrangeEnding",
            "IsCylinder",
            "IsUseCylinder",
            "IsMysticalCylinder",
            "IsUseMysticalCylinder",
            "IsEliteCylinder",
            "IsUseEliteCylinder",
            "IsDeveloperLetter",
        };
        
        private Dictionary<string, int> _intPair = new Dictionary<string, int>();

        private string[] _intHash = Array.Empty<string>();

        private Dictionary<string, string> _stringsPair = new Dictionary<string, string>();

        private string[] _stringsHash = Array.Empty<string>();
        
        public Saver(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public void Save()
        {
            if (!IsSave)
                return;

            if (GameData.CharacterController)
                SavePlayerPosition();
            
            _saveSystem.SaveGameToSlot(1);
            Debug.Log("Игра сохранена");
        }

        public void Load()
        {
            _saveSystem.LoadGameFromSlot(1);
            Debug.Log("Игра загруженна");
        }

        public void LoadLevel()
        {
            if (Lua.IsTrue("Variable[\"IsNewGame\"] == true"))
            {
                Lua.Run("Variable[\"IsNewGame\"] = false");
                GameData.LocationsManager.SwitchLocation("BavWorld", 0);
            }
            else
            {
                GameData.LocationsManager.SwitchLocation(Lua.Run("return Variable[\"LocationName\"]").AsString, Lua.Run("return Variable[\"LocationPointIndex\"]").AsInt);
                GameData.CharacterController.transform.position = LoadPosition();
            }
        }
        
        private void SavePosition(Vector2 value)
        {
            Lua.Run($"Variable[\"PositionX\"] = {value.x}");
            Lua.Run($"Variable[\"PositionY\"] = {value.y}");
        }
        
        public Vector2 LoadPosition()
        {
            var x = Lua.Run("return Variable[\"PositionX\"]").AsFloat;
            var y = Lua.Run("return Variable[\"PositionY\"]").AsFloat;
            return new Vector2(x, y);
        }

        public void Reset()
        {
            _boolPair = new Dictionary<string, bool>();
            
            foreach (var boolHash in _boolHash)
            {
                var value = Lua.IsTrue($"Variable[{boolHash}] == true");
                _boolPair.Add(boolHash, value);
            }
            
            foreach (var intHash in _intHash)
            {
                var value = Lua.Run($"return Variable[{intHash}]").AsInt;
                _intPair.Add(intHash, value);
            }
            
            foreach (var stringHash in _stringsHash)
            {
                var value = Lua.Run($"return Variable[{stringHash}]").AsString;
                _stringsPair.Add(stringHash, value);
            }

            var settingData = YandexGame.savesData.SettingDataJson;
            
            SaveSystem.DeleteSavedGameInSlot(1);
            SaveSystem.ResetGameState();
            YandexGame.savesData.SettingDataJson = SaveSystem.Serialize(new SavedGameData());
            YandexGame.SaveProgress();
            Load();
            
            foreach (var pair in _boolPair) 
                Lua.Run($"Variable[{pair.Key}] = {pair.Value}");

            foreach (var pair in _intPair) 
                Lua.Run($"Variable[{pair.Key}] = {pair.Value}");

            foreach (var pair in _stringsPair) 
                Lua.Run($"Variable[{pair.Key}] = {pair.Value}");

            Lua.Run($"Variable[\"FUN\"] = {Random.Range(1, 11)}");

            YandexGame.savesData.SettingDataJson = settingData;
            Save();
            GameData.Saver.IsSave = true;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex == 0 ? 1 : 0);
        }

        public void SavePlayerPosition()
        {            
            if (!IsSave || !GameData.CharacterController)
                return;
            
            SavePosition(GameData.CharacterController.transform.position);
        }
    }
}