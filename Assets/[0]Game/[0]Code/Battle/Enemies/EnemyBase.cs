using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField]
        protected EnemyConfig _config;

        public EnemyConfig GetConfig => _config;

        public virtual void StartBattle()
        {
            GameData.EnemyData = new EnemyData()
            {
                EnemyConfig = _config,
                Enemy = this,
            };

            if (GetComponent<Collider2D>())
                GetComponent<Collider2D>().enabled = false;
            
            GameData.Battle.StartBattle();
        }
        
        public abstract IEnumerator AwaitCustomEvent(string eventName, float value = 0);
    }
}