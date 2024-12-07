using System;
using UnityEngine;

namespace Game
{
    public class BattleTestStarter : MonoBehaviour
    {
        [SerializeField]
        private EnemyBase _startBattleTrigger;
        
        private void Start()
        {
            _startBattleTrigger.StartBattle();
        }
    }
}