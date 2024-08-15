using UnityEngine;
using YG;

namespace Game
{
    public class TestingTool : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V)) 
                _canvas.gameObject.SetActive(!_canvas.gameObject.activeSelf);
        }

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