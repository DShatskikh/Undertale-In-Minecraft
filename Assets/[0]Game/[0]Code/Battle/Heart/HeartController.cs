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

        private HeartModel _model;
        private Shield _shield;
        private HeartView _view;

        private void Awake()
        {
            _model = new HeartModel();
            _shield = new Shield(_model);
            _view = GetComponent<HeartView>();
            _view.SetModel(_model);
        }

        private void OnEnable()
        {
            _model.IsInvulnerability = false;
        }

        private void Update()
        {
            var position = (Vector2)transform.position;
            
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if (direction == Vector2.zero && GameData.Joystick.Direction.magnitude > 0.5f)
                direction = GameData.Joystick.Direction.normalized;

            position += direction * _speed * Time.deltaTime;

            if (GameData.Arena.activeSelf)
            {
                var limitX = _sizeField.x / 2;
                var limitY = _sizeField.y / 2;
                position = new Vector2(
                    Mathf.Clamp(position.x, -limitX + GameData.Arena.transform.position.x, limitX + GameData.Arena.transform.position.x), 
                    Mathf.Clamp(position.y, -limitY + GameData.Arena.transform.position.y, limitY + GameData.Arena.transform.position.y));
            }
            
            if (!_model.IsInvulnerability)
                _shield.Execute(position);

            transform.position = position;
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