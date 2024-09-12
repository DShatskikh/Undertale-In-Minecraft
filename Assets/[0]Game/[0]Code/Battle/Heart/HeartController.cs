using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using YG;

namespace Game
{
    public class HeartController : MonoBehaviour
    {
        [SerializeField] 
        private Vector2 _sizeField;

        [SerializeField]
        private float _speed;

        [SerializeField] 
        private AudioSource _damageSource;

        [SerializeField]
        private HeartView _view;
        
        private HeartModel _model;
        private Shield _shield;
        private Vector3 _previousPosition;

        private void Awake()
        {
            _model = new HeartModel();
            _shield = new Shield(_model);
            _view.SetModel(_model);
        }

        private void OnEnable()
        {
            _model.IsInvulnerability = false;
        }

        private void OnDisable()
        {
            _model.SetSpeed(0);
        }

        private void Update()
        {
            var position = (Vector2)transform.position;
            
            _model.SetDirection(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized);

            if (_model.Direction == Vector2.zero && GameData.Joystick.Direction.magnitude > 0.5f)
                _model.Direction = GameData.Joystick.Direction.normalized;

            position += _model.Direction * _speed * Time.deltaTime;

            if (GameData.Battle.Arena.activeSelf)
            {
                var limitX = _sizeField.x / 2 - 0.06f;
                var limitY = _sizeField.y / 2 - 0.07f;
                position = new Vector2(
                    Mathf.Clamp(position.x, -limitX + GameData.Battle.Arena.transform.position.x, limitX + GameData.Battle.Arena.transform.position.x), 
                    Mathf.Clamp(position.y, -limitY + GameData.Battle.Arena.transform.position.y, limitY + GameData.Battle.Arena.transform.position.y));
            }
            
            if (!_model.IsInvulnerability)
                _shield.Execute(position.AddY(0.15f));

            transform.position = position;
        }

        private void FixedUpdate()
        {
            _model.SetSpeed(((Vector2)(_previousPosition - transform.position)).magnitude);
            _previousPosition = transform.position;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Shell attack))
            {
                if (!_model.IsInvulnerability) 
                    StartCoroutine(TakeDamage());

                Destroy(attack.gameObject);
            }
        }

        private IEnumerator TakeDamage()
        {
            YandexGame.savesData.Health -= GameData.EnemyData.EnemyConfig.Attack;
            EventBus.Damage?.Invoke(1);
            EventBus.HealthChange?.Invoke(YandexGame.savesData.MaxHealth, YandexGame.savesData.Health);
            _shield.Off();
            _damageSource.Play();

            if (YandexGame.savesData.Health <= 0 && !YandexGame.savesData.IsCheat)
            {
                Death();
                yield break;
            }

            _model.SetIsInvulnerability(true);
            yield return new WaitForSeconds(1);
            _model.SetIsInvulnerability(false);
        }

        private void Death()
        {
            EventBus.Death?.Invoke();
            GameData.GameOver.SetActive(true);

            Analytics.CustomEvent("Death " + GameData.EnemyData.EnemyConfig.name);
        }
    }
}