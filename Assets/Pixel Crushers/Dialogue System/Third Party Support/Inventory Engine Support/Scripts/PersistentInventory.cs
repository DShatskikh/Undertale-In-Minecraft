using UnityEngine;
using System.Reflection;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;

namespace PixelCrushers.DialogueSystem.InventoryEngineSupport
{

    /// <summary>
    /// Includes an Inventory in persistent data.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Dialogue System/Third Party/Inventory Engine/Persistent Inventory")]
    [RequireComponent(typeof(MoreMountains.InventoryEngine.Inventory))]
    public class PersistentInventory : MonoBehaviour
    {

        public string playerID = string.Empty;

        [Tooltip("Dialogue System Lua variable under which to save inventory.")]
        public string savedDataVariableName;

        protected string actualVariableName { get { return string.IsNullOrEmpty(savedDataVariableName) ? name : savedDataVariableName; } }

        public void OnRecordPersistentData()
        {
            // Inventory.FillSerializedInventory is protected, so we need to use reflection:
            var serializedInventory = new SerializedInventory();
            var method = typeof(Inventory).GetMethod("FillSerializedInventory", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(GetComponent<MoreMountains.InventoryEngine.Inventory>(), new object[] { serializedInventory });
            DialogueLua.SetVariable(actualVariableName, JsonUtility.ToJson(serializedInventory));
        }

        public void OnApplyPersistentData()
        {
            // Inventory.ExtractSerializedInventory is protected, so we need to use reflection:
            if (!DialogueLua.DoesVariableExist(actualVariableName)) return;
            var data = DialogueLua.GetVariable(actualVariableName).AsString;
            var serializedInventory = JsonUtility.FromJson<SerializedInventory>(data);
            // JSON converts nulls to empty strings. Need to convert back to nulls:
            for (int i = 0; i < serializedInventory.ContentType.Length; i++)
            {
                if (string.IsNullOrEmpty(serializedInventory.ContentType[i]))
                {
                    serializedInventory.ContentType[i] = null;
                }
            }
            var method = typeof(Inventory).GetMethod("ExtractSerializedInventory", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(GetComponent<MoreMountains.InventoryEngine.Inventory>(), new object[] { serializedInventory });
            MMEventManager.TriggerEvent(new MMInventoryEvent(MMInventoryEventType.InventoryLoaded, null, this.name, null, 0, 0, playerID));
        }

        public void OnEnable()
        {
            PersistentDataManager.RegisterPersistentData(this.gameObject);
        }

        public void OnDisable()
        {
            PersistentDataManager.RegisterPersistentData(this.gameObject);
        }

    }
}