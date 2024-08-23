using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class DancingPig : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private void OnEnable()
        {
            StartCoroutine(AwaitDance());
        }

        private IEnumerator AwaitDance()
        {
            _spriteRenderer.flipX = Random.Range(0, 2) == 1;
            yield return new WaitForSeconds(Random.Range(0, 2f));
            
            while (true)
            {
                yield return AwaitJump();
                yield return AwaitJump();
                yield return new WaitForSeconds(0.5f);
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
                yield return new WaitForSeconds(0.5f);
            }
        }

        private IEnumerator AwaitJump()
        {
            var progress = 0.0f;
            var duration = 0.2f;
            var transform = _spriteRenderer.transform;
            var startPosition = transform.position;
            var endPosition = startPosition.AddY(0.5f);

            while (progress < 1)
            {
                progress += Time.deltaTime / duration;
                transform.position = Vector2.Lerp(startPosition, endPosition, progress);
                yield return null;
            }

            yield return new WaitForSeconds(0.2f);
            progress = 0.0f;
            
            while (progress < 1)
            {
                progress += Time.deltaTime / duration;
                transform.position = Vector2.Lerp(endPosition, startPosition, progress);
                yield return null;
            }
        }
    }
}