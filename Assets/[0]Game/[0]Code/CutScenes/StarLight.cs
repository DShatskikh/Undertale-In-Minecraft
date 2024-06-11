using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class StarLight : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;

        [SerializeField]
        private float _min, _max;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _delay;
        
        private float _currentIntensivity;

        private IEnumerator Start()
        {
            while (true)
            {
                while (_currentIntensivity > _min)
                {
                    _currentIntensivity -= Time.deltaTime * _speed;
                    _spriteRenderer.color = _spriteRenderer.color.SetA(_currentIntensivity);
                    yield return null;
                }

                yield return new WaitForSeconds(_delay);
                
                while (_currentIntensivity < _max)
                {
                    _currentIntensivity += Time.deltaTime * _speed;
                    _spriteRenderer.color = _spriteRenderer.color.SetA(_currentIntensivity);
                    yield return null;
                }
                
                yield return new WaitForSeconds(_delay);
            }
        }
    }
}