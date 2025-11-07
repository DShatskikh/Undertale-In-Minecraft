using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class InMenuButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (YG.YandexGame.savesData.IsErrorWorld)
            {
                GameData.Saver.Reset();
                SceneManager.LoadScene(0);
                return;
            }
            
            GameData.Saver.Save();
            SceneManager.LoadScene(0);
        }
    }
}