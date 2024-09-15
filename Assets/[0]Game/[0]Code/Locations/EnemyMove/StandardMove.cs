using System.Collections;
using UnityEngine;

namespace Game
{
    public class StandardMove : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1.5f, _distance = 3.5f;
        
        [SerializeField]
        private SpriteRenderer _view;

        [SerializeField]
        private Animator _animator;

        private Vector2 _startPosition;
        private bool _isInit;
        private Coroutine _coroutine;

        private void Start()
        {
            _startPosition = transform.position;

            _isInit = true;
        }

        private void OnEnable()
        {
            StartMove();
        }

        public void StartMove()
        {
            _coroutine = StartCoroutine(AwaitMove());
        }
        
        public void StopMove()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _view.flipX = true;
        }
        
        private IEnumerator AwaitMove()
        {
            yield return new WaitUntil(() => _isInit);
            
            while (true)
            {
                var targetPosition = _startPosition.AddX(Random.Range(-_distance, _distance)).AddY(Random.Range(-_distance, _distance));
                _view.flipX = targetPosition.x - transform.position.x < 0;
                _animator.SetFloat("Speed", 1);
                yield return new MoveToPointCommand(transform, targetPosition, Vector2.Distance(transform.position, targetPosition) / _speed).Await();
                _animator.SetFloat("Speed", 0);
                yield return new WaitForSeconds(3f);
            }
        }
    }
}