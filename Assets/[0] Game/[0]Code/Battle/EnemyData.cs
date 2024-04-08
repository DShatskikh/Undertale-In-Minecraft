using UnityEngine;

namespace Game
{
    public class EnemyData
    {
        public EnemyConfig EnemyConfig;
        public GameObject GameObject { get; set; }
        public StartBattleTrigger StartBattleTrigger { get; set; }
    }
}