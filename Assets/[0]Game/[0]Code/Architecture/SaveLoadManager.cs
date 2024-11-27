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
    public class SaveLoadManager
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
        
        public SaveLoadManager(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public void Save()
        {
            if (!IsSave)
                return;
            
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
                //GameData.LocationsManager.SwitchLocation(, 0);
            }
            else
            {
                //GameData.LocationsManager.SwitchLocation(Lua.Run("return Variable[\"LocationName\"]").AsString, Lua.Run("return Variable[\"LocationPointIndex\"]").AsInt);
            }
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

            var fun = Lua.Run("return Variable[FUN]").AsInt;
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

            GenerateFun(fun);

            YandexGame.savesData.SettingDataJson = settingData;
            Save();
            GameData.SaveLoadManager.IsSave = true;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex == 0 ? 1 : 0);
        }

        private void GenerateFun(int currentFun)
        {
            var fun = Random.Range(1, 11);

            if (fun == currentFun)
            {
                fun += 1;

                if (fun > 10)
                    fun = 3;
            }
            
            Lua.Run($"Variable[\"FUN\"] = {fun}");
        }
        
        public static T GetData<T>(string key) where T : new()
        {
            var s = SaveSystem.currentSavedGameData.GetData(key);
            var saveData = new T();
            var data = SaveSystem.Deserialize(s, saveData);
            return data;
        }
    }
}