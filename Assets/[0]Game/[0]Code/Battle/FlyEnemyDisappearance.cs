using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class FlyEnemyDisappearance : EnemyDisappearanceBase
    {
        [SerializeField]
        private Transform _endPoint;

        [SerializeField]
        private float _speed;

        public override void Disappearance(UnityAction action)
        {
            StartCoroutine(AwaitDisappearance(action));
        }

        private IEnumerator AwaitDisappearance(UnityAction action)
        {
            var progress = 0f;
            var startPosition = transform.position;

            while (progress < 1)
            {
                progress += _speed * Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, _endPoint.position, progress);
                yield return null;
            }
            
            gameObject.SetActive(false);
            action.Invoke();
        }
    }
}