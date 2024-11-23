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

        public void Show(float targetA = 1f)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            gameObject.SetActive(true);
            _coroutine = StartCoroutine(AwaitShow(targetA));
        }

        public void Hide()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
         
            if (!gameObject.activeSelf)
                return;
            
            _coroutine = StartCoroutine(AwaitHide());
        }

        public IEnumerator AwaitShow(float targetA)
        {
            var duration = 0f;
            var startA = _spriteRenderer.color.a;
            
            while (duration < 0.5f)
            {
                _spriteRenderer.color = _spriteRenderer.color.SetA(Mathf.Lerp(startA, targetA, duration / 0.5f));
                yield return null;
                duration += Time.deltaTime;
            }
        }
        
        public IEnumerator AwaitHide()
        {
            var duration = 0f;
            var startA = _spriteRenderer.color.a;
            
            while (duration < 0.5f)
            {
                _spriteRenderer.color = _spriteRenderer.color.SetA(Mathf.Lerp(startA, 0f, duration / 0.5f));
                yield return null;
                duration += Time.deltaTime;
            }
        }
    }
}