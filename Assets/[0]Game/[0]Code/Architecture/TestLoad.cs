using PixelCrushers.DialogueSystem;
using UnityEngine;
using YG;

namespace Game
{
    public class TestLoad
    {
        public void Load()
        {
            Lua.Run("Variable[\"KILLS\"] = 5");
            Lua.Run("Variable[\"BlueCowState\"] = 2");
            //GameData.CompanionsManager.TryActivateCompanion("FakeHero");
            
            Lua.Run("Variable[\"IsBavBugKey\"] = true");
            YandexGame.savesData.MaxHealth = 4;
            YandexGame.savesData.Health = 4;

            //Lua.Run("Variable[\"IsDeveloperLetter\"] = true");
            //Lua.Run("Variable[\"IsCylinder\"] = true");
            //Lua.Run("Variable[\"IsMysteryCylinder\"] = true");
            //Lua.Run("Variable[\"IsEliteCylinder\"] = true");
            
            //Lua.Run("Variable[\"IsUseCylinder\"] = true");
            //Lua.Run("Variable[\"IsUseMysteryCylinder\"] = true");
            //Lua.Run("Variable[\"IsUseEliteCylinder\"] = true");
        }
    }
}