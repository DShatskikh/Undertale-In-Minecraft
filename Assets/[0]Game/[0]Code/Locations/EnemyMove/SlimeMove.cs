using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class SlimeMove : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _view;

        [SerializeField]
        private GameObject _shadow;
        
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

            _view.transform.localScale = Vector3.one;
            _view.transform.localPosition = Vector3.zero;
            _view.flipX = true;
            _shadow.SetActive(false);
        }
        
        private IEnumerator AwaitMove()
        {
            yield return new WaitUntil(() => _isInit);
            var viewTransform = _view.transform;
            _shadow.SetActive(true);
            
            while (true)
            {
                yield return new ScaleCommand(viewTransform, viewTransform.localScale.SetY(0.5f), 0.5f).Await();
                yield return new ScaleCommand(viewTransform, viewTransform.localScale.SetY(1.1f), 0.5f).Await();
                
                var targetPosition = _startPosition.AddX(Random.Range(-1.5f, 1.5f)).AddY(Random.Range(-1.5f, 1.5f));
                _view.flipX = targetPosition.x - viewTransform.position.x < 0;
                
                yield return new MoveToPointCommand(viewTransform, viewTransform.position.AddY(2), 0.5f).Await();
                yield return new MoveToPointCommand(transform, targetPosition, Vector2.Distance(transform.position, targetPosition) / 3).Await();
                yield return new MoveToPointCommand(viewTransform, viewTransform.position.AddY(-2), 0.5f).Await();
                yield return new ScaleCommand(viewTransform, viewTransform.localScale.SetY(0.75f), 0.5f).Await();
                yield return new ScaleCommand(viewTransform, Vector2.one, 0.5f).Await();

                yield return new WaitForSeconds(3);
            }
        }
    }
}