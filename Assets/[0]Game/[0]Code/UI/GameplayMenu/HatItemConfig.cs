using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "HatItemConfig", menuName = "Data/Items/Hat", order = 56)]
    public class HatItemConfig : ItemConfig, IUsable
    {
        [SerializeField]
        private string PutOn;
        
        public void Use()
        {
            if (Lua.IsTrue($"Variable[\"{PutOn}\"] == false"))
            {
                Lua.Run($"Variable[\"{PutOn}\"] = true");
                GameData.CharacterController.HatPoint.UpgradeView();
            }
            else
            {
                Lua.Run($"Variable[\"{PutOn}\"] = false");
                GameData.CharacterController.HatPoint.UpgradeView();
            }
        }
    }
}