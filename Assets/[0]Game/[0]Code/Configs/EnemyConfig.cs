using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemy", order = 30)]
    public class EnemyConfig : ScriptableObject
    {
        public BattleArena Arena;
        public AttackBase[] Attacks;
        public BaseActConfig[] Acts;
        public LocalizedString[] BattleReplicas;
        public Replica[] EndReplicas;
        public LocalizedString ProgressLocalized;
    }
}