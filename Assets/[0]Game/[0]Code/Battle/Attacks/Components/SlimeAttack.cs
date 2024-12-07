using System;
using UnityEngine;

namespace Game
{
    public class SlimeAttack : MonoBehaviour
    {
        private Transform _enemyPoint;
        
        private void Start()
        {
            _enemyPoint = GameData.Battle.SessionData.EnemiesOverWorldPositions[0].Point;
            transform.position = _enemyPoint.position;
            _enemyPoint.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _enemyPoint.gameObject.SetActive(true);
        }
    }
}