using System;
using UnityEngine;

namespace Game
{
    public class GameMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            GameData.SubmitUpdater.gameObject.SetActive(false);
            GameData.CancelUpdater.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            GameData.SubmitUpdater.gameObject.SetActive(true);
            GameData.CancelUpdater.gameObject.SetActive(true);
        }
    }
}