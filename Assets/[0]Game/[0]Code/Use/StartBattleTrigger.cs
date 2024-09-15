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

        [SerializeField]
        private bool _isNotTrigger;
        
        public Vector2 Offset => _offset;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isNotTrigger)
                return;
            
            if (!GameData.IsCanStartBattle)
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
                _event.Invoke();
        }
    }
}