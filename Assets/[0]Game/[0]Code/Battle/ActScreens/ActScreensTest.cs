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
        private DanceActScreen _danceActScreen;

        [SerializeField]
        private DanceActConfig _danceActConfig;
        
        [SerializeField]
        private EnemyBase _enemyBase;

        [SerializeField]
        private CharacterController _characterController;

        [SerializeField]
        private Battle _battle;
        
        private void Start()
        {
            GameData.CharacterController = _characterController;
            GameData.Battle = _battle;
            
            GameData.EnemyData = new EnemyData
            {
                Enemy = _enemyBase
            };
            //_attackActScreen.Init(_config);
            //_danceActScreen.Init(_danceActConfig);
            StartCoroutine(_enemyBase.AwaitCustomEvent("Damage", 20));
        }
    }
}