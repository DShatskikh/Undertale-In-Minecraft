using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "CylinderItemConfig", menuName = "Data/Items/Cylinder", order = 56)]
    public class CylinderItemConfig : ItemConfig, IUsable
    {
        public void Use()
        {
            if (Lua.IsTrue("Variable[\"IsUseCylinder\"] == false"))
            {
                Lua.Run("Variable[\"IsUseCylinder\"] = true");
                GameData.CharacterController.HatPoint.CylinderShow(true);
            }
            else
            {
                Lua.Run("Variable[\"IsUseCylinder\"] = false");
                GameData.CharacterController.HatPoint.CylinderShow(false);
            }
        }
    }
}