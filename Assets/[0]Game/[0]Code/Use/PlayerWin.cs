using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class PlayerWin : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _event;

        [SerializeField] 
        private EnemyConfig _config;
        
        private void Start()
        {
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