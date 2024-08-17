using System;
using UnityEngine;

namespace Game
{
    public class BattleTestStarter : MonoBehaviour
    {
        [SerializeField]
        private EnemyConfig _enemyConfig;

        [SerializeField]
        private GameObject _enemyObject;
        
        private void Start()
        {
            GameData.EnemyData = new EnemyData()
            {
                EnemyConfig = _enemyConfig,
                GameObject = _enemyObject
            };

            _enemyObject.transform.position = GameData.EnemyPoint.position;
            GameData.Character.transform.position = GameData.CharacterPoint.position;
            GetComponent<Battle>().StartBattle();
        }
    }
}