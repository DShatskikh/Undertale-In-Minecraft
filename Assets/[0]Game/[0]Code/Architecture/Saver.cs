using PixelCrushers;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Game
{
    public class Saver
    {
        public bool IsSave = true;
        private readonly SaveSystem _saveSystem;

        public Saver(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public void Save()
        {
            if (!IsSave)
                return;

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
            if (Lua.IsTrue("return Variable[\"IsNewGame\"] == true"))
            {
                Lua.Run($"Variable[\"IsNewGame\"] = true");
                GameData.LocationsManager.SwitchLocation("BavWorld", 0);
            }
            else
            {
                GameData.CharacterController.transform.position = LoadPosition();
                GameData.LocationsManager.SwitchLocation(Lua.Run("return Variable[\"LocationName\"]").AsString, Lua.Run("return Variable[\"LocationPointIndex\"]").AsInt);
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
            YandexGame.savesData.ResetAllIntPair();
            
            //YandexGame.savesData.MaxHealth = 20;
            //YandexGame.savesData.LocationName = "HerobrineHome";
            YandexGame.savesData.NumberGame += 1;

            YandexGame.savesData.IsCake = false;
            YandexGame.savesData.IsNotIntroduction = false;
            YandexGame.savesData.IsCheat = false;
            YandexGame.savesData.IsPrisonKey = false;
            YandexGame.savesData.IsDeveloperKey = false;
            YandexGame.savesData.IsSpeakHerobrine = false;
            YandexGame.savesData.IsCapturedWorld = false;
            YandexGame.savesData.IsNotCapturedWorld = false;
            YandexGame.savesData.IsAlternativeWorld = YandexGame.savesData.IsBadEnd && YandexGame.savesData.IsGoodEnd && YandexGame.savesData.IsStrangeEnd 
                                                      && YandexGame.savesData.IsPalesosEnd && Random.Range(1, 6) == 2;
            
            YandexGame.SaveProgress();
            SceneManager.LoadScene(0);
        }

        public void SavePlayerPosition()
        {            
            if (!IsSave)
                return;
            
            SavePosition(GameData.CharacterController.transform.position);
        }
    }
}