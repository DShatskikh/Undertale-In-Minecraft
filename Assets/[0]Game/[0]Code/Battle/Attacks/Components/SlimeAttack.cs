using System;
using UnityEngine;

namespace Game
{
    public class SlimeAttack : MonoBehaviour
    {
        private void Start()
        {
            transform.position = GameData.EnemyPoint.position;
            GameData.EnemyPoint.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            GameData.EnemyPoint.gameObject.SetActive(true);
        }
    }
}