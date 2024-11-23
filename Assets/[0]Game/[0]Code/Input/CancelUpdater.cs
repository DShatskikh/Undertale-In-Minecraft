using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class CancelUpdater : MonoBehaviour
    {
        private void Awake()
        {
            GameData.PlayerInput.actions["Cancel"].performed += CancelInvoke;
            GameData.PlayerInput.actions["Cancel"].canceled +=  CancelUpInvoke;
        }
        
        private void OnDestroy()
        {
            if (GameData.PlayerInput)
            {
                GameData.PlayerInput.actions["Cancel"].performed -= CancelInvoke;
                GameData.PlayerInput.actions["Cancel"].canceled -=  CancelUpInvoke;
            }
        }

        private void CancelInvoke(InputAction.CallbackContext context) => 
            EventBus.Cancel?.Invoke();
        
        private void CancelUpInvoke(InputAction.CallbackContext context) => 
            EventBus.CancelUp?.Invoke();
    }
}