using UnityEngine;

namespace Game
{
    public class ToggleInventoryUpdater : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("ToggleInventory")) 
                EventBus.OpenInventory?.Invoke();

            if (Input.GetButtonUp("ToggleInventory")) 
                EventBus.OpenInventoryUp?.Invoke();
        }
    }
}