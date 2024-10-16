using System;
using UnityEngine;

namespace Game
{
    public class SubmitUpdater : MonoBehaviour
    {
        private void Awake()
        {
            GameData.PlayerInput.actions["Submit"].performed += context => EventBus.Submit?.Invoke();
            GameData.PlayerInput.actions["Submit"].canceled +=  context => EventBus.SubmitUp?.Invoke();
        }

        private void OnDestroy()
        {
            if (GameData.PlayerInput != null)
            {
                GameData.PlayerInput.actions["Submit"].performed -= context => EventBus.Submit?.Invoke();
                GameData.PlayerInput.actions["Submit"].canceled -= context => EventBus.SubmitUp?.Invoke();
            }
        }
    }
}