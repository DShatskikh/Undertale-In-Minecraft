using UnityEngine;
using MoreMountains.InventoryEngine;

namespace PixelCrushers.DialogueSystem.InventoryEngineSupport
{

    /// <summary>
    /// Updates the quest tracker HUD and invokes a UnityEvent when an Inventory's content changes.
    /// Adds option to integrate Dialogue System saving with Inventory Engine SaveLoadManager.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Dialogue System/Third Party/Inventory Engine/Dialogue System Inventory Engine Methods")]
    public class DialogueSystemInventoryEngineMethods : MonoBehaviour
    {

        /// <summary>
        /// Set the UI EventSystem's Send Navigation Events checkbox. Inventory Engine disables
        /// this when inventory is closed, but you may need it open to navigate other UIs.
        /// </summary>
        public void SetEventSystemNavigationEvents(bool value)
        {
            if (UnityEngine.EventSystems.EventSystem.current != null)
            {
                UnityEngine.EventSystems.EventSystem.current.sendNavigationEvents = value;
            }
        }

        /// <summary>
        /// Enables or disables Inventory Engine's input manager to allow opening/closing
        /// of inventories. You might want to disable then when a pause menu is open, for example.
        /// </summary>
        /// <param name="value"></param>
        public void SetInventoryInputManager(bool value)
        {
            var inventoryManager = FindObjectOfType<InventoryInputManager>();
            if (inventoryManager != null) inventoryManager.enabled = value;
        }

    }
}
