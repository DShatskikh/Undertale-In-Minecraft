using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ToColor : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private float _duration;
        
        [SerializeField]
        private Color _color;

        [SerializeField]
        private UnityEvent _event;
        
        public void Use()
        {
            StartCoroutine(AwaitUse());
        }

        private IEnumerator AwaitUse()
        {
            var progress = 0f;
            var startColor = _spriteRenderer.color;

            do
            {
                progress += Time.deltaTime / _duration;
                _spriteRenderer.color = Color.Lerp(startColor, _color, progress);
                yield return null;
            } while (progress < 1f);
            
            _event.Invoke();
        }
    }
}