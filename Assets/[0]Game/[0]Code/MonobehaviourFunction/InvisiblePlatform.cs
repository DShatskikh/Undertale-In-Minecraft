using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class InvisiblePlatform : MonoBehaviour
    {
        private const float Duration = 1f;
        
        private SpriteRenderer _spriteRenderer;
        private Coroutine _coroutine;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _spriteRenderer.color = Color.white.SetA(0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(AwaitChangingTransparency(1f));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(AwaitChangingTransparency(0f));
        }

        private void OnDestroy()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private IEnumerator AwaitChangingTransparency(float endA)
        {
            var progress = 0f;
            var startA = _spriteRenderer.color.a;

            while (progress < 1)
            {
                progress += Time.deltaTime / Duration;
                var a = Mathf.Lerp(startA, endA, progress);
                _spriteRenderer.color = _spriteRenderer.color.SetA(a);
                yield return null;
            }
        }
    }
}