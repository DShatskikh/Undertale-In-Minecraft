using UnityEngine;
using YG;

namespace Game
{
    public class TestingTool : MonoBehaviour 
    {
        [ContextMenu("Полное удаление данных")]
        private void FullReset()
        {
            foreach (var location in GameData.Locations) 
                location.gameObject.SetActive(false);

            GameData.Introduction.SetActive(true);
            YandexGame.savesData.FullReset();
        }
    }
}