using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class StartHint : MonoBehaviour
    {
        private IEnumerator Start()
        {
            var firstPosition = transform.position;

            while (true)
            {
                var progress = 0f;
                var startPosition = transform.position;
                var targetPosition = firstPosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 1f;

                while (progress < 1f)
                {
                    progress += Time.deltaTime / 4;
                    transform.position = Vector2.Lerp(startPosition, targetPosition, progress);
                    yield return null;
                }
            }
        }
    }
}