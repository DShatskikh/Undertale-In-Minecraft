using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class UseReset : MonoBehaviour
    {
        public void Use()
        {
            GameData.SaveLoadManager.Save();
            
            foreach (var saver in FindObjectsByType<SaveLoadBase>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                saver.Reset();

            GameData.SaveLoadManager.Reset();
            SceneManager.LoadScene(0);
        }
    }
}