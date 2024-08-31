using System;

namespace Game
{
    public static class EventBus
    {
        public static Action OnEnemyDead;
        
        /// <summary>
        /// MaxValue, Value
        /// </summary>
        public static Action<int, int> HealthChange;
        public static Action<int> BattleProgressChange;
        public static Action<int> Damage;
        public static Action CloseMonolog;
        public static Action CloseDialog;
        public static Action<EnemyConfig> PlayerWin;
        public static Action Death;
        public static Action Cancel;
        public static Action Submit;
        public static Action ToggleInventory;
    }
}