using PixelCrushers.DialogueSystem;

namespace Game
{
    public class TestLoad
    {
        public void Load()
        {
            Lua.Run("Variable[\"KILLS\"] = 5");
            Lua.Run("Variable[\"BlueCowState\"] = 1");
            //GameData.CompanionsManager.TryActivateCompanion("FakeHero");
        }
    }
}