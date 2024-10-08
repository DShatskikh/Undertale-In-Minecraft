namespace Game
{
    public class GameplayMenuSlotModel
    {
        public GameplayMenuConfig Config => _config;
        
        private GameplayMenuConfig _config;
        
        public GameplayMenuSlotModel(GameplayMenuConfig config)
        {
            _config = config;
        }
    }
}