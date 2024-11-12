using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Reload : MonoBehaviour
    {
        public void Use()
        {
            SceneManager.LoadScene(1);
        }
    }
}