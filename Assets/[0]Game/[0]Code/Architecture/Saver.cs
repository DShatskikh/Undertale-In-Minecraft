using UnityEngine;
using YG;

namespace Game
{
    public class Saver
    {
        public void Save()
        {
            SavePlayerPosition();
            YandexGame.SaveProgress();
            Debug.Log("Игра сохранена");
        }

        public void Load()
        {
            YandexGame.LoadProgress();
            Debug.Log("Игра загруженна");
        }

        private void SavePosition(Vector2 value)
        {
            YandexGame.savesData.PositionX = value.x;
            YandexGame.savesData.PositionY = value.y;
            
            Debug.Log("Позиция сохранена");
        }
        
        public Vector2 LoadPosition()
        {
            Debug.Log("Позиция загруженна");
            return new Vector2(YandexGame.savesData.PositionX, YandexGame.savesData.PositionY);
        }

        public void Reset()
        {
            YandexGame.savesData.ResetAllIntPair();
            
            YandexGame.savesData.MaxHealth = 20;
            YandexGame.savesData.LocationIndex = 0;
            YandexGame.savesData.NumberGame += 1;

            YandexGame.savesData.IsHat = false;
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
        }

        public void SavePlayerPosition()
        {
            SavePosition(GameData.CharacterController.transform.position);
        }
    }
}