using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class SlimeMover : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _view;

        [SerializeField]
        private GameObject _shadow;

        private Vector2 _startPosition;
        private Coroutine _coroutine;
        private Sequence _jumpAnimation;

        private void OnEnable()
        {
            StartMove();
        }

        private void OnDisable()
        {
            StopMove();
        }

        public void StartMove()
        {
            _startPosition = transform.position;
            _coroutine = StartCoroutine(AwaitMove());
        }

        public void StopMove()
        {
            _jumpAnimation.Kill();
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _view.transform.localScale = Vector3.one;
            _view.transform.localPosition = Vector3.zero;
            _view.flipX = true;
            _shadow.SetActive(false);
        }

        private IEnumerator AwaitMove()
        {
            var viewTransform = _view.transform;
            _shadow.SetActive(true);

            while (true)
            {
                var targetPosition = _startPosition.AddX(Random.Range(-1.5f, 1.5f)).AddY(Random.Range(-1.5f, 1.5f));
                _view.flipX = targetPosition.x - viewTransform.position.x < 0;
                var nextPoint = (Vector2)transform.position + (targetPosition - (Vector2)transform.position).normalized * 2;
            
                _jumpAnimation = DOTween.Sequence();
                
                yield return _jumpAnimation
                    .Append(viewTransform.DOScaleY(0.5f, 0.5f))
                    .Append(viewTransform.DOScaleY(1.1f, 0.5f))
                    .Insert(0.9f, viewTransform.DOLocalMoveY(2f, 1f))
                    .Insert(0.9f, transform.DOMove(nextPoint, 1f))
                    .Insert(1.2f, viewTransform.DOLocalMoveY(0f, 1f))
                    .Insert(1.7f,viewTransform.DOScaleY(0.75f, 0.5f))
                    .Append(viewTransform.DOScaleY(1f, 0.5f))
                    .WaitForCompletion();

                yield return new WaitForSeconds(1);
            }
            
            /*
            yield return new WaitUntil(() => _isInit);
            _isMove = true;
            
            var viewTransform = _view.transform;
            _shadow.SetActive(true);
            
            while (_isMove)
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
            }*/
        }

        public IEnumerator AwaitPlayDown()
        {
            var viewTransform = _view.transform;
            yield return new ScaleCommand(viewTransform, viewTransform.localScale.SetY(0.75f), 0.5f).Await();
            yield return new ScaleCommand(viewTransform, Vector2.one, 0.5f).Await();
        }
    }
}