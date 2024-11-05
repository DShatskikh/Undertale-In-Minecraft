using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class ChickenExtras : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Coroutine _coroutine;
        private bool _isFlipX;
        private float _startY;
        
        private void Start()
        {
            _isFlipX = _spriteRenderer.flipX;
            _startY = transform.position.y;
            StartMove();
        }

        public void StartMove()
        {
            _coroutine = StartCoroutine(AwaitMove());
        }
        
        public void StopMove()
        {
            StopCoroutine(_coroutine);
        }

        private IEnumerator AwaitMove()
        {
            while (true)
            {
                var indexEvent = Random.Range(0, 2);

                switch (indexEvent)
                {
                    case 0:
                        _spriteRenderer.flipX = !_isFlipX;
                        yield return new WaitForSeconds(1);
                        _spriteRenderer.flipX = _isFlipX;
                        break;
                    case 1:
                        int countJump = Random.Range(1, 4);

                        for (int i = 0; i < countJump; i++)
                        {
                            var moveUpCommand = new MoveToPointCommand(_spriteRenderer.transform, _spriteRenderer.transform.position.SetY(_startY + 1f), 0.5f);
                            yield return moveUpCommand.Await();
                            var moveDownCommand = new MoveToPointCommand(_spriteRenderer.transform, _spriteRenderer.transform.position.SetY(_startY), 0.25f);
                            yield return moveDownCommand.Await();
                        }
                        
                        break;
                    default:
                        break;
                }
            
                yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            }
        }
    }
}