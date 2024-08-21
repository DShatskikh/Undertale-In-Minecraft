using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class DesiccantFly : MonoBehaviour
    {
        [SerializeField]
        private Transform _startPoint, _endPoint, _transform;

        [SerializeField]
        private float _moveSpeed;
        
        [SerializeField]
        private float _ySpeed = 5;
        
        [SerializeField]
        private float _amplitude = 1;
        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        private IEnumerator Start()
        {
            while (true)
            {
                var process = 0f;
                _spriteRenderer.flipX = false;
                
                while (process < 1)
                {
                    process += Time.deltaTime * _moveSpeed;
                    _transform.position = Vector2.Lerp(_startPoint.position, _endPoint.position, process)
                        .SetY(Mathf.Sin(process * _ySpeed) * _amplitude + _startPoint.position.y);

                    yield return null;
                }
                
                process = 0f;
                _spriteRenderer.flipX = true;
            
                while (process < 1)
                {
                    process += Time.deltaTime * _moveSpeed;
                    _transform.position = Vector2.Lerp(_endPoint.position, _startPoint.position, process)
                        .SetY(Mathf.Sin(process * _ySpeed) * _amplitude + _startPoint.position.y);;
                    yield return null;
                }
            }
        }
    }
}