using System.Collections;
using UnityEngine;

namespace Game
{
    public abstract class StarBaseShell : Shell
    {
        [SerializeField]
        private SpriteRenderer _view;
        
        [SerializeField]
        private SpriteRenderer _line1;
        
        [SerializeField]
        private SpriteRenderer _line2;

        private float _direction;
        
        private IEnumerator Start()
        {
            _view.color = _view.color.SetA(0);
            var changeAlphaCommand = new ChangeAlphaCommand(_view, 1, 1);
            yield return changeAlphaCommand.Await();
        }

        public void StartMove()
        {
            StartCoroutine(AwaitMove());
        }

        private IEnumerator AwaitMove()
        {
            var scaleBigCommand = new ScaleCommand(transform, transform.localScale * 1.25f, 0.25f);
            yield return scaleBigCommand.Await();
            
            var scaleMiniCommand = new ScaleCommand(transform, transform.localScale / 1.25f, 0.25f);
            yield return scaleMiniCommand.Await();
            
            _line1.gameObject.SetActive(true);
            _line2.gameObject.SetActive(true);

            var direction = GetDirection();

            _line1.transform.position -= direction * 0.04f;
            _line2.transform.position -= direction * 2 * 0.04f;
            
            while (true)
            {
                transform.position += direction * Time.deltaTime * 4;
                yield return null;
            }
        }

        protected abstract Vector3 GetDirection();
    }
}