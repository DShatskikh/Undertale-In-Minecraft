using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SmoothDisappearance : MonoBehaviour
    {
        [SerializeField] 
        private float _duration = 1;

        [SerializeField] 
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField]
        private UnityEvent _event;
        
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

                if (alpha < 0)
                    alpha = 0;
            }
            
            _event.Invoke();
        }

        public void SetDuration(float value)
        {
            _duration = value;
        }
    }
}