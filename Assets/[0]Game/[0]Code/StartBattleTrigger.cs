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

        public Vector2 Offset => _offset;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character character))
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
            EventBus.OnPlayerWin += OnPlayerWin;
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