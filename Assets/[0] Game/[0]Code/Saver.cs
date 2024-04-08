using UnityEngine;

namespace Game
{
    public class Saver
    {
        public void Save()
        {
            SavePlayerPosition();
            
            PlayerPrefs.SetInt("MaxHealth", GameData.MaxHealth);
            PlayerPrefs.SetInt("Palesos", GameData.Palesos);
            PlayerPrefs.SetInt("LocationIndex", GameData.LocationIndex);
            PlayerPrefs.SetFloat("Volume", GameData.Volume);

            SetBool("IsNotFirstPlay", GameData.IsNotFirstPlay);
            SetBool("IsCheat", GameData.IsCheat);
            SetBool("IsPrisonKey", GameData.IsPrisonKey);
            SetBool("IsGoldKey", GameData.IsGoldKey);
            SetBool("IsDeveloperKey", GameData.IsDeveloperKey);
            SetBool("IsSpeakHerobrine", GameData.IsSpeakHerobrine);
            SetBool("IsCapturedWorld", GameData.IsCapturedWorld);
            SetBool("IsNotCapturedWorld", GameData.IsNotCapturedWorld);
            SetBool("IsHat", GameData.IsHat);
            SetBool("IsGoodEnd", GameData.IsGoodEnd);
            SetBool("IsBadEnd", GameData.IsBadEnd);
            SetBool("IsStrangeEnd", GameData.IsStrangeEnd);
            SetBool("IsNotIntroduction", GameData.IsNotIntroduction);
            
            Debug.Log("Игра сохранена");
        }

        public void Load()
        {
            GameData.MaxHealth = PlayerPrefs.GetInt("MaxHealth", 20);
            GameData.Palesos = PlayerPrefs.GetInt("Palesos");
            GameData.LocationIndex = PlayerPrefs.GetInt("LocationIndex");
            
            GameData.Volume = PlayerPrefs.GetFloat("Volume");
            
            GameData.IsHat = GetBool("IsHat");
            GameData.IsNotFirstPlay = GetBool("IsNotFirstPlay");
            GameData.IsCheat = GetBool("IsCheat");
            GameData.IsPrisonKey = GetBool("IsPrisonKey");
            GameData.IsGoldKey = GetBool("IsGoldKey");
            GameData.IsDeveloperKey = GetBool("IsDeveloperKey");
            GameData.IsSpeakHerobrine = GetBool("IsSpeakHerobrine");
            GameData.IsCapturedWorld = GetBool("IsCapturedWorld");
            GameData.IsNotCapturedWorld = GetBool("IsNotCapturedWorld");
            GameData.IsGoodEnd = GetBool("IsGoodEnd");
            GameData.IsBadEnd = GetBool("IsBadEnd");
            GameData.IsStrangeEnd = GetBool("IsStrangeEnd");
            GameData.IsNotIntroduction = GetBool("IsNotIntroduction");
            
            Debug.Log("Игра загруженна");
        }

        private void SavePosition(Vector2 value)
        {
            PlayerPrefs.SetFloat("PositionX", value.x);
            PlayerPrefs.SetFloat("PositionY", value.y);
            
            Debug.Log("Позиция сохранена");
        }
        
        public Vector2 LoadPosition()
        {
            Debug.Log("Позиция загруженна");
            return new Vector2(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"));
        }

        private bool GetBool(string key)
        {
            return PlayerPrefs.GetInt(key) == 1;
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