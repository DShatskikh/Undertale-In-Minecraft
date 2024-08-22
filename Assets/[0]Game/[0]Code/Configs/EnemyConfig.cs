using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemy", order = 30)]
    public class EnemyConfig : ScriptableObject
    {
        public AttackBase[] Attacks;
        public int Attack = 3;
        public int WinPrize = 2;
        public Act[] Acts;
        public BattleMessageData[] EndReplicas;
    }
}