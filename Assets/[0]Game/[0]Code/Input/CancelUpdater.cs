using System;
using UnityEngine;

namespace Game
{
    public class CancelUpdater : MonoBehaviour
    {
        private void Awake()
        {
            GameData.PlayerInput.actions["Cancel"].performed += context => EventBus.Cancel?.Invoke();
            GameData.PlayerInput.actions["Cancel"].canceled +=  context => EventBus.CancelUp?.Invoke();
        }
        
        private void OnDestroy()
        {
            GameData.PlayerInput.actions["Cancel"].performed -= context => EventBus.Cancel?.Invoke();
            GameData.PlayerInput.actions["Submit"].canceled -=  context => EventBus.CancelUp?.Invoke();
        }
    }
}