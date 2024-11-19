using PixelCrushers.DialogueSystem;
using YG;

namespace Game
{
    public class TestLoad
    {
        public void Load()
        {
            //Lua.Run("Variable[\"KILLS\"] = 5");
            //Lua.Run("Variable[\"BlueCowState\"] = 1");
            //Lua.Run("Variable[\"IsCylinder\"] = true");
            Lua.Run("Variable[\"IsBavBugKey\"] = true");
            YandexGame.savesData.MaxHealth = 4;
            YandexGame.savesData.Health = 4;
            //GameData.CompanionsManager.TryActivateCompanion("FakeHero");
        }
    }
}