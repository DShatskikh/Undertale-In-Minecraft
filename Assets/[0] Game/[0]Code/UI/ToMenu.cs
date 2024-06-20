using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(0);
        }
    }
}