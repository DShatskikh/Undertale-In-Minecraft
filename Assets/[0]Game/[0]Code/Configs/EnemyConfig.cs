using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace Game
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemy", order = 30)]
    public class EnemyConfig : ScriptableObject
    {
        public AttackBase[] Attacks;
        public int Attack = 3;
        public int WinPrize = 2;
        public BaseActConfig[] Acts;
        [FormerlySerializedAs("Acts")]
        public Act[] OldActs;
        public LocalizedString[] EndReplicas;
    }
}