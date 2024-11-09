using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SmoothAppearance : MonoBehaviour
    {
        [SerializeField] 
        private float _duration = 1;

        [SerializeField, Range(0f, 1f)]
        private float _maxValue = 1f;
        
        [SerializeField]
        private UnityEvent _event;
        
        public IEnumerator Start()
        {
            var _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = _spriteRenderer.color.SetA(0);
            
            var alpha = _spriteRenderer.color.a;
            
            while (alpha < _maxValue)
            {
                alpha = _spriteRenderer.color.a;
                alpha += Time.deltaTime / _duration;
                _spriteRenderer.color = _spriteRenderer.color.SetA(alpha);
                yield return null;
            }
            
            _event.Invoke();
        }
    }
}