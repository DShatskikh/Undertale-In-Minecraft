using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Game
{
    public class ToMenu : MonoBehaviour
    {
        public void Use()
        {
            Invoke(nameof(Load), 0);
        }

        private void Load()
        {
            YandexGame.SaveProgress();
            SceneManager.LoadScene(0);
        }
    }
}