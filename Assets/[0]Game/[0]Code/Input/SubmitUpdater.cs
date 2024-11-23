using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class SubmitUpdater : MonoBehaviour
    {
        private void Awake()
        {
            GameData.PlayerInput.actions["Submit"].performed += Submit;
            GameData.PlayerInput.actions["Submit"].canceled += SubmitUp;
        }

        private void OnDestroy()
        {
            if (GameData.PlayerInput != null)
            {
                GameData.PlayerInput.actions["Submit"].performed -= Submit;
                GameData.PlayerInput.actions["Submit"].canceled -= SubmitUp;
            }
        }

        private void Submit(InputAction.CallbackContext context) => 
            EventBus.Submit?.Invoke();

        private void SubmitUp(InputAction.CallbackContext context) => 
            EventBus.SubmitUp?.Invoke();
    }
}