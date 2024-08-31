using UnityEngine;

namespace Game
{
    public class ToggleInventoryUpdater : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("ToggleInventory"))
            {
                EventBus.ToggleInventory?.Invoke();
            }
        }
    }
}