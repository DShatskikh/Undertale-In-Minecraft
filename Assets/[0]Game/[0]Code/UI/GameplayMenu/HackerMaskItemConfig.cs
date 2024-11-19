using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "HackerMaskItemConfig", menuName = "Data/Items/HackerMask", order = 55)]
    public class HackerMaskItemConfig : ItemConfig, IUsable
    {
        public void Use()
        {
            if (Lua.IsTrue("Variable[\"IsUseHackerMask\"] == false"))
            {
                Lua.Run("Variable[\"IsUseHackerMask\"] = true");
                GameData.CharacterController.HatPoint.MaskShow(true);
            }
            else
            {
                Lua.Run("Variable[\"IsUseHackerMask\"] = false");
                GameData.CharacterController.HatPoint.MaskShow(false);
            }
        }
    }
}