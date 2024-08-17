using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class StartBattleTrigger : MonoBehaviour
    {
        [SerializeField]
        private EnemyConfig _config;

        [SerializeField] 
        private GameObject _enemyObject;

        [SerializeField] 
        private Vector2 _offset;

        [SerializeField]
        private UnityEvent _event;

        private bool _isCanStartBattle;
        public Vector2 Offset => _offset;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            _isCanStartBattle = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isCanStartBattle)
                return;
            
            if (other.TryGetComponent(out CharacterController character))
            {
                StartBattle();
            }
        }

        public void StartBattle()
        {
            GameData.EnemyData = new EnemyData()
            {
                EnemyConfig = _config,
                GameObject = _enemyObject,
                StartBattleTrigger = this
            };

            if (GetComponent<Collider2D>())
                GetComponent<Collider2D>().enabled = false;
            
            GameData.Battle.StartBattle();
            EventBus.PlayerWin += OnPlayerWin;
        }

        private void OnPlayerWin(EnemyConfig config)
        {
            if (_config == config)
            {
                _event.Invoke();
            }
        }
    }
}