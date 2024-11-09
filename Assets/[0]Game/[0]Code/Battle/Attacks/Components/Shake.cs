using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Shake : MonoBehaviour
    {
        [SerializeField, Range(0.001f, 1)]
        private float duration;
        
        [SerializeField, Range(0.001f, 1)]
        private float magnitude;

        private Coroutine _coroutine;

        private void OnEnable()
        {
            _coroutine = StartCoroutine(AwaitShake());
        }
        
        private void OnDisable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        public void SetShake(float shaking)
        {
            magnitude = shaking;
        }

        private IEnumerator AwaitShake()
        {
            yield return null;
            
            while (true)
            {
                Vector3 originalPos = transform.localPosition;
                float elapsed = 0.0f;

                while (elapsed < duration)
                {
                    float x = Random.Range(-1f, 1f) * magnitude;
                    float y = Random.Range(-1f, 1f) * magnitude;

                    transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

                    elapsed += Time.deltaTime;

                    yield return null;
                }

                transform.localPosition = originalPos;
            }
        }
    }
}