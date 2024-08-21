using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Symbol : MonoBehaviour
    {
        private Coroutine _coroutine;
        private const float Duration = 4f;

        private void OnEnable()
        {
            StartCoroutine(AwaitAnimation());
        }

        private IEnumerator AwaitAnimation()
        {
            var minSize = 1;
            var maxSize = 3;
            
            while (true)
            {
                transform.localScale = Vector2.zero;
                yield return new WaitForSeconds(Random.Range(0f, Duration));
                
                if (_coroutine != null)
                    StopCoroutine(_coroutine);
                
                _coroutine = StartCoroutine(ShowAndHide());

                var progress = 0.0f;
                
                while (progress < 1)
                {
                    progress += Time.deltaTime / Duration;
                    transform.localScale = Vector2.Lerp(Vector2.one * minSize, Vector2.one * maxSize, progress);
                    yield return null;
                }
            }
        }

        private IEnumerator ShowAndHide()
        {
            var group = GetComponent<CanvasGroup>();
            var progress = 0.0f;
                
            while (progress < 1)
            {
                progress += Time.deltaTime;
                group.alpha = progress;
                yield return null;
            }

            yield return Duration - 2;

            progress = 0.0f;
                
            while (progress < 1)
            {
                progress += Time.deltaTime;
                group.alpha = 1 - progress;
                yield return null;
            }
        }
    }
}