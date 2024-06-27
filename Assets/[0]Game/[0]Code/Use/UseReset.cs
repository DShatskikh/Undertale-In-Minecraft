﻿using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(0);
        }
    }
}