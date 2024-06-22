using UnityEngine;

namespace Game
{
    public class UseReset : MonoBehaviour
    {
        public void Use()
        {
            GameData.Saver.Save();
            
            foreach (var saver in FindObjectsByType<SaveLoadBase>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                saver.Reset();

            GameData.Saver.Reset();
            GameData.Saver.Load();
        }
    }
}