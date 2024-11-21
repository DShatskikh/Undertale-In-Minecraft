using System.Collections;
using UnityEngine;

namespace Game
{
    public class FireballShell : Shell
    {
        [SerializeField]
        private SpriteRenderer _warning;

        [SerializeField]
        private Transform _fireball;

        [SerializeField]
        private GameObject _explosion;
        
        [SerializeField]
        private GameObject _fire;
        
        private IEnumerator Start()
        {
            GetComponent<Collider2D>().enabled = false;
            
            _fireball.gameObject.SetActive(false);
            _fire.SetActive(false);

            _fireball.position = transform.position.AddY(10);
            
            _warning.gameObject.SetActive(true);
            
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.2f);
                _warning.color = Color.red;
                yield return new WaitForSeconds(0.2f);
                _warning.color = Color.yellow;
            }
            
            _warning.gameObject.SetActive(false);

            //yield return new WaitForSeconds(0.5f);

            _fireball.gameObject.SetActive(true);
            
            var moveToPointCommand = new MoveToPointCommand(_fireball, transform.position, 1.5f);
            yield return moveToPointCommand.Await();

            _fireball.gameObject.SetActive(false);
            _explosion.SetActive(true);
            GetComponent<Collider2D>().enabled = true;

            yield return new WaitForSeconds(0.4f);
            _fire.SetActive(true);
            
            yield return new WaitForSeconds(0.2f);
            _explosion.SetActive(false);
        }
    }
}