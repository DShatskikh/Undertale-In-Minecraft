using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;

namespace Game
{
    public class Heart : MonoBehaviour
    {
        [SerializeField] 
        private Vector2 _sizeField;

        [SerializeField]
        private float _speed;
        
        [SerializeField]
        private Shield _shield;

        [SerializeField] 
        private AudioSource _damageSource;
        
        private bool _isInvulnerability;

        private void Update()
        {
            var position = (Vector2)transform.position;
            
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if (direction == Vector2.zero && GameData.Joystick.Direction.magnitude > 0.5f)
            {
                direction = GameData.Joystick.Direction.normalized; 
            }

            position += direction * _speed * Time.deltaTime;

            if (GameData.Arena.activeSelf)
            {
                var limitX = _sizeField.x / 2;
                var limitY = _sizeField.y / 2;
                position = new Vector2(
                    Mathf.Clamp(position.x, -limitX + GameData.Arena.transform.position.x, limitX + GameData.Arena.transform.position.x), 
                    Mathf.Clamp(position.y, -limitY + GameData.Arena.transform.position.y, limitY + GameData.Arena.transform.position.y));
            }

            transform.position = position;
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
            EventBus.OnDamage?.Invoke(1);
            EventBus.OnHealthChange?.Invoke(GameData.MaxHealth, GameData.Health);
            _damageSource.Play();
            _shield.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            _shield.gameObject.SetActive(true);
            _isInvulnerability = false;
            
            if (GameData.Health <= 0 && !GameData.IsCheat)
                Death();
        }

        private void Death()
        {
            EventBus.OnDeath?.Invoke();
            GameData.GameOver.SetActive(true);

            Analytics.CustomEvent("Death " + GameData.EnemyData.EnemyConfig.name);
        }
    }
}