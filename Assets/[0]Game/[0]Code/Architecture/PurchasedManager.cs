using System.Collections.Generic;
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
                    YandexMetrica.Send("BuyCylinder");
                    break;
                case "MysteryCylinder":
                    Lua.Run("Variable[\"IsMysteryCylinder\"] = true");
                    YandexMetrica.Send("BuyMysteryCylinder");
                    break;
                case "EliteCylinder":
                    Lua.Run("Variable[\"IsEliteCylinder\"] = true");
                    YandexMetrica.Send("BuyEliteCylinder");
                    break;
                default:
                    break;
            }
        }
    }
}