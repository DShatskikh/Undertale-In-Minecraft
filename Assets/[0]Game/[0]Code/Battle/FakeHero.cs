using System.Collections;
using UnityEngine;

namespace Game
{
    public class FakeHero : EnemyBase
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        public override IEnumerator AwaitCustomEvent(string eventName)
        {
            if (eventName == "StartBattle")
            {
                _spriteRenderer.flipX = true;
            }

            if (eventName == "Damage")
            {
                print("Damage");
                yield return null;
            }
        }
    }
}