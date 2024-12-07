using System.Collections;
using UnityEngine;

namespace Game
{
    public class BlackPanel : MonoBehaviour
    {
        private Coroutine _coroutine;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Reset()
        {
            gameObject.SetActive(true);
            _spriteRenderer.color = _spriteRenderer.color.SetA(0);
        }

        public void Show(float targetA = 1f, float duration = 0.5f)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            gameObject.SetActive(true);
            _coroutine = StartCoroutine(AwaitShow(targetA, duration));
        }

        public void Hide(float duration = 0.5f)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
         
            if (!gameObject.activeSelf)
                return;
            
            _coroutine = StartCoroutine(AwaitHide(duration));
        }

        public IEnumerator AwaitShow(float targetA, float duration = 0.5f)
        {
            var progress = 0f;
            var startA = _spriteRenderer.color.a;
            
            while (progress < 1f)
            {
                yield return null;
                progress += Time.deltaTime / duration;
                _spriteRenderer.color = _spriteRenderer.color.SetA(Mathf.Lerp(startA, targetA, progress));
            }
            
            _spriteRenderer.color = _spriteRenderer.color.SetA(targetA);
        }
        
        public IEnumerator AwaitHide(float duration = 0.5f)
        {
            var progress = 0f;
            var startA = _spriteRenderer.color.a;
            
            while (progress < 0.5f)
            {
                _spriteRenderer.color = _spriteRenderer.color.SetA(Mathf.Lerp(startA, 0f, progress / duration));
                yield return null;
                progress += Time.deltaTime;
            }
        }
    }
}