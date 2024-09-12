using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BlackPanel : MonoBehaviour
    {
        private Coroutine _coroutine;
        private SpriteRenderer _spriteRenderer;
        private float _targetA;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Show(float targetA = 0.88f)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _targetA = targetA;
            
            gameObject.SetActive(true);
            _coroutine = StartCoroutine(AwaitShow());
        }

        public void Hide()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
         
            if (!gameObject.activeSelf)
                return;
            
            _coroutine = StartCoroutine(AwaitHide());
        }

        private IEnumerator AwaitShow()
        {
            var duration = 0f;
            var startA = _spriteRenderer.color.a;
            
            while (duration < 0.5f)
            {
                _spriteRenderer.color = _spriteRenderer.color.SetA(Mathf.Lerp(startA, _targetA, duration / 0.5f));
                yield return null;
                duration += Time.deltaTime;
            }
        }
        
        private IEnumerator AwaitHide()
        {
            var duration = 0f;
            var startA = _spriteRenderer.color.a;
            
            while (duration < 0.5f)
            {
                _spriteRenderer.color = _spriteRenderer.color.SetA(Mathf.Lerp(startA, 0f, duration / 0.5f));
                yield return null;
                duration += Time.deltaTime;
            }
            
            gameObject.SetActive(false);
        }
    }
}