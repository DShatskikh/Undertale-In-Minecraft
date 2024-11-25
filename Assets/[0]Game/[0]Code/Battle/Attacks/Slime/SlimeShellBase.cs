using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class SlimeShellBase : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _view;

        [SerializeField]
        private Sprite _comicSprite;
        
        private void Awake()
        {
            //SetActive(false);

            if (Random.Range(0, 15) == 5)
            {
                _view.sprite = _comicSprite;
            }
        }

        protected IEnumerator AwaitMoveToPoint(Vector3 targetPosition)
        {
            var viewTransform = _view.transform;
            _view.flipX = targetPosition.x - viewTransform.position.x > 0;
                
            yield return new ScaleCommand(viewTransform, viewTransform.localScale.SetY(0.5f), 0.25f).Await();
            yield return new ScaleCommand(viewTransform, viewTransform.localScale.SetY(1.1f), 0.25f).Await();

            //SetActive(false);

            var y = 0.5f;

            Vector3 nextPoint = (targetPosition - transform.position).normalized * 1f;
            
            StartCoroutine(new MoveLocalCommand(viewTransform, new Vector2(0, y), 0.25f).Await());
            yield return new WaitForSeconds(0.1f);
            
            yield return new MoveToPointCommand(transform, transform.position + nextPoint, 0.75f).Await();

            StartCoroutine( new MoveLocalCommand(viewTransform, Vector2.zero, 0.25f).Await());
            yield return new WaitForSeconds(0.1f);

            //SetActive(true);
                
            yield return new ScaleCommand(viewTransform, viewTransform.localScale.SetY(0.75f), 0.25f).Await();
            yield return new ScaleCommand(viewTransform, Vector2.one, 0.25f).Await();

            yield return new WaitForSeconds(0.5f);
        }
    }
}