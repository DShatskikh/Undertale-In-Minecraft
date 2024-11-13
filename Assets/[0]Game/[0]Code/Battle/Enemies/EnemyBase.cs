using System.Collections;
using UnityEngine;

namespace Game
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField]
        protected EnemyConfig _config;

        public EnemyConfig GetConfig => _config;
        public abstract IEnumerator AwaitCustomEvent(string eventName, float value = 0);
    }
}