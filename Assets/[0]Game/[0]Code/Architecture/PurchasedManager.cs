using PixelCrushers.DialogueSystem;
using UnityEngine;
using YG;

namespace Game
{
    public class PurchasedManager
    {
        public PurchasedManager()
        {
            YandexGame.PurchaseSuccessEvent += PurchaseSuccessEvent;
        }

        ~PurchasedManager()
        {
            YandexGame.PurchaseSuccessEvent -= PurchaseSuccessEvent;
        }

        private void PurchaseSuccessEvent(string obj)
        {
            Debug.Log($"Buy: {obj}");
            YandexGame.savesData.IsBuySupport = true;

            switch (obj)
            {
                case "Cylinder":
                    Lua.Run("Variable[\"IsCylinder\"] = true");
                    break;
                case "MysteryCylinder":
                    Lua.Run("Variable[\"IsMysteryCylinder\"] = true");
                    break;
                case "EliteCylinder":
                    Lua.Run("Variable[\"IsEliteCylinder\"] = true");
                    break;
                default:
                    break;
            }
        }
    }
}