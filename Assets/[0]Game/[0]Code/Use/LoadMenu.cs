using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LoadMenu : MonoBehaviour
    {
        public void Use()
        {
            SceneManager.LoadScene(0);
        }
    }
}