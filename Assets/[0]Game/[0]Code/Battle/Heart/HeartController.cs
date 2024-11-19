using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;
using YG;

namespace Game
{
    public class HeartController : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        
        [SerializeField]
        private HeartView _view;

        [SerializeField]
        private Rigidbody2D _rigidbody;
        
        private HeartModel _model;
        private Shield _shield;
        private Vector3 _previousPosition;
        
        public HeartView View => _view;

        private void Awake()
        {
            _model = new HeartModel();
            _shield = new Shield(_model);
            _view.SetModel(_model);
        }

        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            _model.SetDirection(obj.ReadValue<Vector2>().normalized);
        }

        private void OnEnable()
        {
            _model.IsInvulnerability = false;
            GameData.PlayerInput.actions["Move"].performed += OnMovePerformed;
        }

        private void OnDisable()
        {
            _model.SetSpeed(0);
            GameData.PlayerInput.actions["Move"].performed -= OnMovePerformed;
        }

        private void Update()
        {
            var position = (Vector2)transform.position;

            if (!_model.IsInvulnerability)
                _shield.Execute(position.AddY(0.15f));
        }

        private void FixedUpdate()
        {
            if (_model.Direction == Vector2.zero && GameData.Joystick.Direction.magnitude > 0.5f)
                _model.Direction = GameData.Joystick.Direction.normalized;

            _rigidbody.position += _model.Direction * _speed / 100;
            
            _model.SetSpeed(((Vector2)(_previousPosition - transform.position)).magnitude);
            _previousPosition = transform.position;
        }
        
        private IEnumerator TakeDamage()
        {
            YandexGame.savesData.Health -= GameData.EnemyData.EnemyConfig.Attack;
            EventBus.Damage?.Invoke(1);
            EventBus.HealthChange?.Invoke(YandexGame.savesData.MaxHealth, YandexGame.savesData.Health);
            _shield.Off();
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.HurtSound);
            
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
            GameData.CommandManager.StopExecute();
            GameData.Battle.gameObject.SetActive(false);
            GameData.GameOver.gameObject.SetActive(true);

            Analytics.CustomEvent("Death " + GameData.EnemyData.EnemyConfig.name);
        }

        public void Damage()
        {
            if (!_model.IsInvulnerability) 
                StartCoroutine(TakeDamage());
        }
    }
}