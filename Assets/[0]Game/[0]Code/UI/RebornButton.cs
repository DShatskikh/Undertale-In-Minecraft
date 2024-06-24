using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class RebornButton : MonoBehaviour
    {
        public void Use()
        {
            SceneManager.LoadScene(1);
        }
    }
}