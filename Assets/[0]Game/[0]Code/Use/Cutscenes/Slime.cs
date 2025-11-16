using System.Collections;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

namespace Game
{
    public sealed class Slime : MonoBehaviour
    {
        private const string SAVE_KEY = "Slime";

        [SerializeField]
        private SpriteRenderer _view;
        
        [SerializeField]
        private Animator _animator;
        
        private StartBattleTrigger _startBattleTrigger;

        private Vector2 _startPoint;

        private void Awake()
        {
            _startBattleTrigger = GetComponent<StartBattleTrigger>();
            _startPoint = transform.position;
        }

        private void Start()
        {
            var value = YandexGame.savesData.GetInt(SAVE_KEY) == 1;
            
            if (value)
                gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            StartCoroutine(AwaitMove());
        }

        private void OnDisable()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character character))
            {
                StopJumping();
                _startBattleTrigger.StartBattle();
                EventBus.PrePlayerWin += OnPlayerWin;
            }
        }

        private void OnPlayerWin(EnemyConfig config)
        {
            if (_startBattleTrigger.GetConfig == config)
            {
                
            }
        }

        public void StopJumping()
        {
            StopAllCoroutines();
            enabled = false;
            _view.flipX = true;
        }
        
        private IEnumerator AwaitMove()
        {
            while (true)
            {
                var targetPoint = _startPoint;

                if (Vector2.Distance(transform.position, _startPoint) > 10)
                {
                    targetPoint += new Vector2(1 * (_startPoint.x > transform.position.x ? 1 : -1), 1 * (_startPoint.y > transform.position.y ? 1 : -1));
                }
                else
                {
                    targetPoint += new Vector2(1 * (Random.Range(0, 2) == 0 ? 1 : -1), 1 * (Random.Range(0, 2) == 0 ? 1 : -1));
                }
                
                _animator.SetTrigger("Jump");
                _view.flipX = targetPoint.x < transform.position.x;
                yield return new WaitForSeconds(0.7f);
                
                GetComponent<BoxCollider2D>().enabled = false;
                _view.sortingOrder = 5;

                yield return new WaitForSeconds(0.4f);
                
                while (Vector2.Distance(transform.position, targetPoint) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPoint, Time.deltaTime * 2);
                    yield return null;
                }

                yield return new WaitForSeconds(0.4f);
                _view.sortingOrder = 0;
                GetComponent<BoxCollider2D>().enabled = true;
                yield return new WaitForSeconds(3.1f);
            }
        }
    }
}