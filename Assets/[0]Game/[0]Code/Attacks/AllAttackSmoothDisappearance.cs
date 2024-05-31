using System.Collections;
using UnityEngine;

namespace Game
{
    public class AllAttackSmoothDisappearance : MonoBehaviour
    {
        [SerializeField] 
        private float _duration = 1;
        
        public IEnumerator Start()
        {
            var spritesRenderer = GetComponentsInChildren<SpriteRenderer>();
            var alpha = 1f;
            
            foreach (var sprite in spritesRenderer)
            {
                while (alpha != 0)
                {
                    if (alpha > sprite.color.a)
                        continue;
                    
                    alpha -= Time.deltaTime / _duration;
                    sprite.color = sprite.color.SetA(alpha);
                    yield return null;
                }
            }
        }
    }
}