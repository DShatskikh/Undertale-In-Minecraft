using System.Collections;
using UnityEngine;

namespace Game
{
    public class SlimeShellBigJump : Shell
    {
        [SerializeField]
        private SpriteRenderer _view;

        [SerializeField]
        private SlimeToCharacterShell _characterShell;
        
        public IEnumerator AwaitMoveToPoint()
        {
            var targetPosition = GameData.HeartController.transform.position;
            
            var viewTransform = _view.transform;
            _view.flipX = targetPosition.x - viewTransform.position.x > 0;
                
            yield return new ScaleCommand(viewTransform, viewTransform.localScale.SetY(0.5f), 0.25f).Await();
            yield return new ScaleCommand(viewTransform, viewTransform.localScale.SetY(1.1f), 0.25f).Await();

            SetActive(false);

            var y = 10f;

            Vector3 nextPoint = (targetPosition - transform.position).normalized * 1f;
            
            StartCoroutine(new MoveLocalCommand(viewTransform, new Vector2(0, y), 1.5f).Await());
            yield return new WaitForSeconds(0.1f);
            
            yield return new MoveToPointCommand(transform, targetPosition, 1.5f).Await();

            StartCoroutine( new MoveLocalCommand(viewTransform, Vector2.zero, 1.5f).Await());
            yield return new WaitForSeconds(0.1f);

            SetActive(true);
                
            yield return new ScaleCommand(viewTransform, viewTransform.localScale.SetY(0.75f), 0.25f).Await();
            yield return new ScaleCommand(viewTransform, Vector2.one, 0.25f).Await();

            yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(1f);

            enabled = false;
            _characterShell.enabled = true;
        }
    }
}