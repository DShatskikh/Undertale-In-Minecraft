using System;
using System.Collections;
using Super_Auto_Mobs;
using UnityEngine;

namespace Game
{
    public class SmoothDisappearance : MonoBehaviour
    {
        [SerializeField] 
        private float _duration = 1;

        [SerializeField] 
        private SpriteRenderer _spriteRenderer;
        
        public IEnumerator Start()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            
            var alpha = _spriteRenderer.color.a;
            
            while (alpha != 0)
            {
                alpha = _spriteRenderer.color.a;
                alpha -= Time.deltaTime / _duration;
                _spriteRenderer.color = _spriteRenderer.color.SetA(alpha);
                yield return null;
            }
        }

        public void SetDuration(float value)
        {
            _duration = value;
        }
    }
}