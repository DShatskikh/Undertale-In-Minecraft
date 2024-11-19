using System.Collections;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

namespace Game
{
    public class FakeHero : EnemyBase
    {
        [SerializeField]
        private DamageEvent _damageEvent;
        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public override IEnumerator AwaitCustomEvent(string eventName, float value = 0)
        {
            if (eventName == "StartBattle")
            {
                _spriteRenderer.flipX = true;
            }

            if (eventName == "Damage")
            {
                yield return _damageEvent.AwaitEvent(this, value);
            }

            if (eventName == "EndBattle")
            {
                    
            }
        }
    }
}