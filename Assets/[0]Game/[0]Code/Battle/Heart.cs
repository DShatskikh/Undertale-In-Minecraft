using System.Collections;
using MoreMountains.Feedbacks;
using Super_Auto_Mobs;
using UnityEngine;
using UnityEngine.Analytics;

namespace Game
{
    public class Heart : MonoBehaviour
    {
        [SerializeField]
        private Shield _shield;

        [SerializeField] 
        private AudioSource _damageSource;

        [SerializeField]
        private Pistol _pistol;

        [SerializeField]
        private CharacterView _view;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private CharacterMover _characterMover;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        
        [SerializeField]
        private float _pushForce;

        [SerializeField]
        private SmokeEffect _smokeEffect;
        
        [SerializeField]
        private MMF_Player _feedback;
        
        private bool _isInvulnerability;
        private bool _isMove;

        private void Update()
        {
            if (!_pistol.gameObject.activeSelf)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _shield.gameObject.SetActive(true);
                    _view.SetBlock(true);
                }
            
                if (Input.GetMouseButtonUp(1))
                {
                    _shield.gameObject.SetActive(false);
                    _view.SetBlock(false);
                }
            }
            
            if (!_shield.gameObject.activeSelf)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _pistol.gameObject.SetActive(true);
                    _view.SetAim(true);
                }
                
                if (Input.GetMouseButtonUp(0))
                {
                    _pistol.TryShot();
                    _pistol.gameObject.SetActive(false);
                    _view.SetAim(false);
                }
            }
            
            if (!_shield.gameObject.activeSelf && !_pistol.gameObject.activeSelf)
            {
                _isMove = true;
                Move();
                _view.Step();
            }
            else if (_isMove)
            {
                _characterMover.Move(Vector2.zero);
                _isMove = false;
                _view.Idle();
            }
        }

        private void Move()
        {
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (direction.x > 0)
                _spriteRenderer.transform.localScale = transform.localScale.SetX(1);
            else if (direction.x < 0)
                _spriteRenderer.transform.localScale = transform.localScale.SetX(-1);
            
            _characterMover.Move(direction);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Attack attack))
            {
                if (!_isInvulnerability)
                {
                    _isInvulnerability = true;
                    StartCoroutine(TakeDamage());
                }

                Destroy(attack.gameObject);
            }
        }

        private IEnumerator TakeDamage()
        {
            GameData.Health -= GameData.EnemyData.EnemyConfig.Attack;
            
            MMF_FloatingText floatingText = _feedback.GetFeedbackOfType<MMF_FloatingText>();
            floatingText.Value = $"-{GameData.EnemyData.EnemyConfig.Attack}";
            floatingText.AnimateColorGradient.colorKeys = new GradientColorKey[] { new(Color.red, 0) };
            _feedback.PlayFeedbacks(transform.position);
            
            EventBus.OnDamage?.Invoke(1);
            EventBus.OnHealthChange?.Invoke(GameData.MaxHealth, GameData.Health);
            _damageSource.Play();
            _shield.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            
            if (Input.GetMouseButton(1))
                _shield.gameObject.SetActive(true);
            
            _isInvulnerability = false;
            
            if (GameData.Health <= 0)
                Death();
        }

        private void Death()
        {
            EventBus.OnDeath?.Invoke();
            GameData.GameOver.SetActive(true);

            Analytics.CustomEvent("Death " + GameData.EnemyData.EnemyConfig.name);
        }

        public void Push(Vector2 direction)
        {
            _rigidbody2D.velocity =(Vector3)direction * _pushForce;
            _smokeEffect.TryUse();
        }
    }
}