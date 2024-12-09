using UnityEngine;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;

namespace PixelCrushers.DialogueSystem.InventoryEngineSupport
{

    /// <summary>
    /// Adds Lua functions to work with Inventory Engine.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Dialogue System/Third Party/Inventory Engine/Inventory Engine Lua (on Dialogue Manager)")]
    public class InventoryEngineLua : MonoBehaviour
    {

        [Tooltip("Cache item info on start to avoid delay when using first Lua function.")]
        public bool initItemCacheOnStart = true;

        public bool debug;

        private Dictionary<string, Inventory> inventoryCache = new Dictionary<string, Inventory>();
        private bool didIRegister = false;

        private static bool s_registered = false;
        private static Dictionary<string, InventoryItem> itemCache = null; // InitItemCache() will create.

        void OnEnable()
        {
            if (!s_registered)
            {
                didIRegister = true;
                s_registered = true;
                Lua.RegisterFunction("mmAddItem", this, SymbolExtensions.GetMethodInfo(() => mmAddItem(string.Empty, string.Empty, (double)0)));
                Lua.RegisterFunction("mmRemoveItem", this, SymbolExtensions.GetMethodInfo(() => mmRemoveItem(string.Empty, string.Empty, (double)0)));
                Lua.RegisterFunction("mmGetQuantity", this, SymbolExtensions.GetMethodInfo(() => mmGetQuantity(string.Empty, string.Empty)));
                Lua.RegisterFunction("mmUseItem", this, SymbolExtensions.GetMethodInfo(() => mmUseItem(string.Empty, string.Empty)));
                Lua.RegisterFunction("mmDropItem", this, SymbolExtensions.GetMethodInfo(() => mmDropItem(string.Empty, string.Empty)));
                Lua.RegisterFunction("mmEquipItem", this, SymbolExtensions.GetMethodInfo(() => mmEquipItem(string.Empty, string.Empty)));
                Lua.RegisterFunction("mmUnEquipItem", this, SymbolExtensions.GetMethodInfo(() => mmUnEquipItem(string.Empty, string.Empty)));
                Lua.RegisterFunction("mmEmptyInventory", this, SymbolExtensions.GetMethodInfo(() => mmEmptyInventory(string.Empty)));
                Lua.RegisterFunction("mmGetNumFreeSlots", this, SymbolExtensions.GetMethodInfo(() => mmGetNumFreeSlots(string.Empty)));
                Lua.RegisterFunction("mmGetNumFilledSlots", this, SymbolExtensions.GetMethodInfo(() => mmGetNumFilledSlots(string.Empty)));
                Lua.RegisterFunction("mmResizeSlots", this, SymbolExtensions.GetMethodInfo(() => mmResizeSlots(string.Empty, (double)0, (double)0)));
                if (initItemCacheOnStart)
                {
#if UNITY_2019_1_OR_NEWER && UNITY_EDITOR
                    if (debug) Debug.Log("Dialogue System: Initializing Inventory Engine item cache. Assets in certain Package Manager packages may report the error 'The referenced script on this Behaviour (Game Object 'AuthObjPrefab') is missing!'. This is harmless and can be ignored.");
#endif
                    InitItemCache();
                }
            }
        }

        void OnDisable()
        {
            if (didIRegister)
            {
                didIRegister = false;
                s_registered = false;
                Lua.UnregisterFunction("mmAddItem");
                Lua.UnregisterFunction("mmRemoveItem");
                Lua.UnregisterFunction("mmGetQuantity");
                Lua.UnregisterFunction("mmUseItem");
                Lua.UnregisterFunction("mmDropItem");
                Lua.UnregisterFunction("mmEquipItem");
                Lua.UnregisterFunction("mmUnEquipItem");
                Lua.UnregisterFunction("mmEmptyInventory");
            }
        }

        public Inventory FindInventory(string inventoryName)
        {
            if (inventoryCache.ContainsKey(inventoryName) && inventoryCache[inventoryName] != null)
            {
                return inventoryCache[inventoryName];
            }
            var go = GameObject.Find(inventoryName) ?? Tools.GameObjectHardFind(inventoryName);
            var inventory = (go != null) ? go.GetComponent<Inventory>() : null;
            if (inventory != null)
            {
                if (inventoryCache.ContainsKey(inventoryName))
                {
                    inventoryCache[inventoryName] = inventory;
                }
                else
                {
                    inventoryCache.Add(inventoryName, inventory);
                }
            }
            if (inventory == null && Debug.isDebugBuild) Debug.LogWarning("Dialogue System: Can't find Inventory GameObject named '" + inventoryName + "'.", this);
            return inventory;
        }

        public InventoryItem FindItem(string itemID, bool logWarningIfNotFound = true)
        {
            if (string.IsNullOrEmpty(itemID)) return null;
            InitItemCache();
            InventoryItem item = null;
            if (itemCache.TryGetValue(itemID, out item))
            {
                return item;
            }
            item = (DialogueManager.LoadAsset(itemID) as InventoryItem) ?? (DialogueManager.LoadAsset("Items/" + itemID) as InventoryItem);
            if (item != null)
            {
                itemCache[itemID] = item;
                return item;
            }
            else
            { 
                if (logWarningIfNotFound && Debug.isDebugBuild) Debug.LogWarning("Dialogue System: Can't find item type '" + itemID + "' in a Resources folder or AssetBundle.");
                return null;
            }
        }

        public static void InitItemCache()
        {
            if (itemCache == null)
            {
                // If no cache yet, build it:
                itemCache = new Dictionary<string, InventoryItem>();
                var itemAssets = Resources.LoadAll<InventoryItem>("");
                for (int i = 0; i < itemAssets.Length; i++)
                {
                    itemCache[itemAssets[i].ItemID] = itemAssets[i];
                }
                itemAssets = Resources.LoadAll<InventoryItem>("Items");
                for (int i = 0; i < itemAssets.Length; i++)
                {
                    itemCache[itemAssets[i].ItemID] = itemAssets[i];
                }
            }
        }

        public int FindItemIndex(Inventory inventory, string itemID)
        {
            if (inventory == null || inventory.Content == null) return -1;
            for (int i = 0; i < inventory.Content.Length; i++)
            {
                var content = inventory.Content[i];
                if (content == null) continue;
                if (string.Equals(content.ItemID, itemID)) return i;
            }
            return -1;
        }

        public void mmAddItem(string inventoryName, string itemID, double quantity)
        {
            try
            {
                if (debug) Debug.Log("Dialogue System: mmAddItem('" + inventoryName + "', '" + itemID + "', " + quantity + ")");
                var inventory = FindInventory(inventoryName);
                if (inventory == null)
                {
                    if (Debug.isDebugBuild) Debug.LogWarning("Dialogue System: mmAddItem() can't find inventory named " + inventoryName);
                    return;
                }

                // Try to find item in Resources/Items:
                bool logWarningIfNotFoundInResources = true;
#if USE_ADDRESSABLES
                logWarningIfNotFoundInResources = false;
#endif
                var itemToAdd = FindItem(itemID, logWarningIfNotFoundInResources);
                if (itemToAdd != null)
                {
                    inventory.AddItem(itemToAdd, (int)quantity);
                    return;
                }

#if USE_ADDRESSABLES
                // Otherwise try addressables:
                DialogueManager.LoadAsset(itemID, typeof(InventoryItem), (asset) =>
                {
                    var inventoryItem = asset as InventoryItem;
                    if (inventoryItem == null)
                    {
                        if (Debug.isDebugBuild) Debug.LogWarning("Dialogue System: mmAddItem() can't find item " + itemID + " in Resources, AssetBundle, or Addressables.");
                        return;
                    }
                    else
                    {
                        itemCache[itemID] = inventoryItem;
                        inventory.AddItem(inventoryItem, (int)quantity);
                    }
                });
#endif
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw e;
            }
            //try
            //{
            //    if (debug) Debug.Log("Dialogue System: mmAddItem('" + inventoryName + "', '" + itemID + "', " + quantity + ")");
            //    var inventory = FindInventory(inventoryName);
            //    if (inventory == null) return;
            //    var itemToAdd = FindItem(itemID);
            //    if (itemToAdd == null) return;
            //    inventory.AddItem(itemToAdd, (int)quantity);
            //}
            //catch (System.Exception e)
            //{
            //    Debug.LogException(e);
            //    throw e;
            //}
        }

        public void mmRemoveItem(string inventoryName, string itemID, double quantity)
        {
            try
            {
                if (debug) Debug.Log("Dialogue System: mmRemoveItem('" + inventoryName + "', '" + itemID + "', " + quantity + ")");
                var inventory = FindInventory(inventoryName);
                if (inventory == null) return;
                if (inventory.Content == null) return;
                var leftToRemove = (int)quantity;
                for (int i = 0; i < inventory.Content.Length; i++)
                {
                    var content = inventory.Content[i];
                    if (content == null) continue;
                    if (string.Equals(content.ItemID, itemID))
                    {
                        var amountToRemoveFromSlot = Mathf.Min(leftToRemove, content.Quantity);
                        leftToRemove -= amountToRemoveFromSlot;
                        inventory.RemoveItem(i, amountToRemoveFromSlot);
                        if (leftToRemove <= 0) return;
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw e;
            }
        }

        public double mmGetQuantity(string inventoryName, string itemID)
        {
            try
            {
                var inventory = FindInventory(inventoryName);
                if (inventory == null) return 0;
                var quantity = inventory.GetQuantity(itemID);
                if (debug) Debug.Log("Dialogue System: mmGetQuantity('" + inventoryName + "', '" + itemID + "') returns " + quantity);
                return inventory.GetQuantity(itemID);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw e;
            }
        }

        public void mmUseItem(string inventoryName, string itemID)
        {
            try
            {
                if (debug) Debug.Log("Dialogue System: mmUseItem('" + inventoryName + "', '" + itemID + ")");
                var inventory = FindInventory(inventoryName);
                if (inventory == null) return;
                inventory.UseItem(itemID);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw e;
            }
        }

        public void mmDropItem(string inventoryName, string itemID)
        {
            mmRemoveItem(inventoryName, itemID, 1);
        }

        public void mmEquipItem(string inventoryName, string itemID)
        {
            try
            {
                if (debug) Debug.Log("Dialogue System: mmEquipItem('" + inventoryName + "', '" + itemID + ")");
                var inventory = FindInventory(inventoryName);
                if (inventory == null) return;
                var item = FindItem(itemID);
                var itemIndex = FindItemIndex(inventory, itemID);
                if (item == null || itemIndex == -1) return;
                inventory.EquipItem(item, itemIndex);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw e;
            }
        }

        public void mmUnEquipItem(string inventoryName, string itemID)
        {
            try
            {
                if (debug) Debug.Log("Dialogue System: mmUnEquipItem('" + inventoryName + "', '" + itemID + ")");
                var inventory = FindInventory(inventoryName);
                if (inventory == null) return;
                var item = FindItem(itemID);
                var itemIndex = FindItemIndex(inventory, itemID);
                if (item == null || itemIndex == -1) return;
                inventory.UnEquipItem(item, itemIndex);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw e;
            }
        }

        public void mmEmptyInventory(string inventoryName)
        {
            try
            {
                if (debug) Debug.Log("Dialogue System: mmEmptyInventory()");
                var inventory = FindInventory(inventoryName);
                if (inventory == null) return;
                inventory.EmptyInventory();
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw e;
            }
        }

        public double mmGetNumFreeSlots(string inventoryName)
        {
            try
            {
                if (debug) Debug.Log("Dialogue System: mmGetNumFreeSlots( " + inventoryName + ")");
                var inventory = FindInventory(inventoryName);
                if (inventory == null) return 0;
                return inventory.NumberOfFreeSlots;
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw e;
            }
        }

        public double mmGetNumFilledSlots(string inventoryName)
        {
            try
            {
                if (debug) Debug.Log("Dialogue System: mmGetNumFilledSlots( " + inventoryName + ")");
                var inventory = FindInventory(inventoryName);
                if (inventory == null) return 0;
                return inventory.NumberOfFilledSlots;
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw e;
            }
        }

        public void mmResizeSlots(string inventoryName, double columns, double rows)
        {
            try
            {
                if (debug) Debug.Log("Dialogue System: mmResizeSlots( " + inventoryName + ", " + columns + ", " + rows + ")");
                var inventory = FindInventory(inventoryName);
                if (inventory == null) return;
                foreach (InventoryDisplay inventoryDisplay in FindObjectsOfType<InventoryDisplay>())
                {
                    if (inventoryDisplay.TargetInventory == inventory)
                    {
                        
                        inventoryDisplay.NumberOfColumns = (int)columns;
                        inventoryDisplay.NumberOfRows = (int)rows;
                        inventoryDisplay.SetupInventoryDisplay();
                        return;
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw e;
            }
        }

    }
}