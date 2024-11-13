using System.Collections;
using UnityEngine;

namespace Game
{
    public class Bee : EnemyBase
    {
        [SerializeField]
        private DamageEvent _damageEvent;
        
        public override IEnumerator AwaitCustomEvent(string eventName, float value = 0)
        {
            if (eventName == "Damage")
            {
                yield return _damageEvent.AwaitEvent(_config, value);
            }
        }
    }
}