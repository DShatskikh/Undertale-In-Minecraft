using System;

namespace Game
{
    public static class EventBus
    {
        public static Action OnEnemyDead;
        
        /// <summary>
        /// MaxValue, Value
        /// </summary>
        public static Action<int, int> OnHealthChange;
        
        /// <summary>
        /// MaxValue, Value
        /// </summary>
        public static Action<int, int> OnEnemyHealthChange;
        
        public static Action<int> OnBattleProgressChange;
        public static Action<int> OnDamage;
        public static Action OnCloseMonolog;
        public static Action OnCloseDialog;
        public static Action<EnemyConfig> OnPlayerWin;
        public static Action OnDeath { get; set; }
        public static Action OnSubmit;
    }
}