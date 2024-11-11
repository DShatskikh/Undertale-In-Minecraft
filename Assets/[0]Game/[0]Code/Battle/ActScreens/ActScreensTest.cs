using System;
using UnityEngine;

namespace Game
{
    public class ActScreensTest : MonoBehaviour
    {
        [SerializeField]
        private AttackActScreen _attackActScreen;

        [SerializeField]
        private AttackActConfig _config;

        [SerializeField]
        private EnemyBase _enemyBase;
        
        private void Start()
        {
            GameData.EnemyData = new EnemyData
            {
                Enemy = _enemyBase
            };
            _attackActScreen.Init(_config);
        }
    }
}