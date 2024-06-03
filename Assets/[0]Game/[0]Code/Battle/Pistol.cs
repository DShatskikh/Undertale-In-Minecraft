using System;
using System.Collections.Generic;
using Cinemachine;
using Super_Auto_Mobs;
using UnityEngine;

namespace Game
{
    public class Pistol : MonoBehaviour
    {
        [SerializeField]
        private Transform _hand;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private float _cooldown;
        
        private RaycastHit2D _target = new RaycastHit2D();
        private List<RaycastHit2D> _targets = new List<RaycastHit2D>();
        private float _currentCooldown;

        private void Update()
        {
            Vector3 mousePosition = Input.mousePosition.SetZ(-10);
            Vector2 point = _camera.ScreenToWorldPoint(mousePosition);
            var position = (Vector2)transform.position;
            var direction = (position - point).normalized;
            
            print("Input.mousePosition " + mousePosition);
            print("point " + point);
            print("position " + position);
            print("direction " + direction);

            RaycastHit2D[] hits = Physics2D.RaycastAll(position, direction, 100f);
            Debug.DrawRay(position, direction * 100f, Color.red, 1f);
            
            float angle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward);
            _hand.rotation = Quaternion.Euler(0, 0, angle + 180);
            _target = new RaycastHit2D();
            _targets = new List<RaycastHit2D>();
            float minDistance = float.MaxValue;
            
            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.TryGetComponent(out Attack attack))
                {
                    float distance = Vector2.Distance(transform.position, hit.point);

                    if (minDistance > distance)
                        _target = hit;
                    
                    _targets.Add(hit);
                }
            }
        }

        public void TryShot()
        {
            GameData.BattleProgress -= 6;
            
            if (_target.collider != null)
            {
                foreach (var target in _targets)
                {
                    Destroy(target.transform.gameObject);
                    GameData.BattleProgress += 3;
                }
            }
            else
            {
                
            }
            
            EventBus.OnBattleProgressChange?.Invoke(GameData.BattleProgress);
        }
    }
}