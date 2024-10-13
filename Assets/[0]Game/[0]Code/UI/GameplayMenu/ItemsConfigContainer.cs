using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ItemsConfigContainer", menuName = "Data/Items/ItemsConfigContainer", order = 59)]
    public class ItemsConfigContainer : ScriptableObject
    {
        public ItemConfig[] Configs;

        public ItemConfig[] GetAvailableItems()
        {
            var configs = new List<ItemConfig>();

            foreach (var config in Configs)
            {
                if (Lua.IsTrue($"Variable[\"{config.Id}\"] == true"))
                    configs.Add(config);
            }
            
            return configs.ToArray();
        }
    }
}