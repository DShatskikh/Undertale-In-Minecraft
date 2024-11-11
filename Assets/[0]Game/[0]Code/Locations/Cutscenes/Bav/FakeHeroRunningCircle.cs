using System.Collections;
using UnityEngine;

namespace Game
{
    public class FakeHeroRunningCircle : MonoBehaviour
    {
        [SerializeField]
        private Animator _fakeHero;

        [SerializeField]
        private Transform _previousFakeHero;
        
        [SerializeField]
        private Transform[] _points;

        [SerializeField]
        private Transform _dragon;

        [SerializeField]
        private SpriteRenderer _dragonShadow, _dragonView;
        
        private IEnumerator Start()
        {
            StartCoroutine(AwaitDragonMove());
            
            _fakeHero.transform.position = _previousFakeHero.position;
            _previousFakeHero.gameObject.SetActive(false);

            while (true)
            {
                foreach (var point in _points)
                {
                    _fakeHero.GetComponent<SpriteRenderer>().flipX = _fakeHero.transform.position.x > point.position.x;
                    _fakeHero.SetFloat("Speed", 1);
                    var moveFakeHeroToPointCommand = new MoveToPointCommand(_fakeHero.transform, point.position, 3);
                    yield return moveFakeHeroToPointCommand.Await();
                    _fakeHero.SetFloat("Speed",  0);

                    yield return new WaitForSeconds(0.5f);
                }
            }
        }

        private IEnumerator AwaitDragonMove()
        {
            _dragon.gameObject.SetActive(true);
            
            while (true)
            {
                var indexPoint = Random.Range(0, _points.Length);
                var flip = _points[indexPoint].position.x < _dragonShadow.transform.position.x;
                _dragonShadow.flipX = flip;
                _dragonView.flipX = flip;

                var moveDragonToHeroCommand = new MoveToPointCommand(_dragon, _points[indexPoint].position, 1f);
                yield return moveDragonToHeroCommand.Await();
            }
        }
    }
}