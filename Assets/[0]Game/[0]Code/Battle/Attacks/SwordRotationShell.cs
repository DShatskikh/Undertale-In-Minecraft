using System.Collections;
using UnityEngine;

namespace Game
{
    public class SwordRotationShell : Shell
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField]
        private SpriteRenderer _line;

        private IEnumerator Start()
        {
            _line.gameObject.SetActive(false);
            
            var direction = ((Vector2) GameData.HeartController.transform.position - (Vector2)transform.position).normalized;
            float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.Rotate(0, 0, rotationZ + 90);

            _spriteRenderer.color = _spriteRenderer.color.SetA(0);
            
            var changeAlphaShowCommand = new ChangeAlphaCommand(_spriteRenderer, 1, 0.5f);
            StartCoroutine(changeAlphaShowCommand.Await());

            var rotationCommand = new RotationCommand(transform, new Vector3(0, 0, rotationZ), 0.5f);
            yield return rotationCommand.Await();
            
            _line.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(0.1f);

            var moveToBackCommand = new MoveToPointCommand(transform, transform .position + (transform.right * -0.5f), 0.25f);
            yield return moveToBackCommand.Await();

            yield return new WaitForSeconds(0.1f);
            
            _line.gameObject.SetActive(false);
            GetComponent<Collider2D>().enabled = true;
            
            var moveToForwardCommand = new MoveToPointCommand(transform, transform .position + (transform.right * 15), 1.44f);
            StartCoroutine(moveToForwardCommand.Await());

            yield return new WaitForSeconds(1.2f);
            GetComponent<Collider2D>().enabled = false;
            
            var changeAlphaHideCommand = new ChangeAlphaCommand(_spriteRenderer, 0, 0.25f);
            yield return changeAlphaHideCommand.Await();

            Destroy(gameObject);
        }
    }
}