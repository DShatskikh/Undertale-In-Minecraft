using PixelCrushers.DialogueSystem;
using UnityEngine;
using YG;

namespace Game
{
    public class TestLoad
    {
        public void Load()
        {
            Lua.Run("Variable[\"FUN\"] = 1");
            //Lua.Run("Variable[\"KILLS\"] = 5");
            Lua.Run("Variable[\"BlueCowState\"] = 3");
            //Lua.Run("Variable[\"IsBavGoodEnding\"] = false");
            Lua.Run("Variable[\"FakeHeroState\"] = 6");
            GameData.CompanionsManager.TryActivateCompanion("FakeHero");
            
            //Lua.Run("Variable[\"IsBavBugKey\"] = true");

            //Lua.Run("Variable[\"IsDeveloperLetter\"] = true");
            //Lua.Run("Variable[\"IsCylinder\"] = true");
            //Lua.Run("Variable[\"IsMysteryCylinder\"] = true");
            //Lua.Run("Variable[\"IsEliteCylinder\"] = true");
            
            //Lua.Run("Variable[\"IsUseCylinder\"] = true");
            //Lua.Run("Variable[\"IsUseMysteryCylinder\"] = true");
            //Lua.Run("Variable[\"IsUseEliteCylinder\"] = true");
            
            Debug.Log(Lua.IsTrue("Variable[\"IsBavGoodEnding\"] ~= true"));
            Debug.Log(Lua.IsTrue("Variable[\"IsBavGoodEnding\"] ~= false"));
            Debug.Log(Lua.IsTrue("Variable[\"IsBavBadEnding\"] ~= false"));
            Debug.Log(Lua.IsTrue("Variable[\"IsBavBadEnding\"] ~= true"));
            Debug.Log(Lua.IsTrue("Variable[\"IsBavBadEnding\"] == true"));
            Debug.Log(Lua.IsTrue("not Variable[\"IsBavBadEnding\"] == true"));
            Debug.Log(Lua.IsTrue("Variable[\"IsBavBadEnding\"] == false or Variable[\"IsBavBadEnding\"] == false"));
            Debug.Log(Lua.IsTrue("(Variable[\"IsBavBadEnding\"] == false) or (Variable[\"IsBavBadEnding\"] == false)"));
        }
    }
}