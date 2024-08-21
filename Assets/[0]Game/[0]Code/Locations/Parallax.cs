using System;
using UnityEngine;

namespace Game
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField]
        private float _parallaxEffect;
        
        [SerializeField]
        private Transform _moon;
        
        [SerializeField]
        private Transform _startPoint;
        
        [SerializeField]
        private Transform _endPoint;

        private Transform _target;

        private void Start()
        {
            _target = Camera.main.transform;
        }

        private void Update()
        {
            float progress = (_startPoint.position.x + _target.position.x) / _endPoint.position.x;
            _moon.transform.position = _moon.transform.position.SetX(Mathf.Lerp(_startPoint.position.x, _endPoint.position.x, progress * _parallaxEffect));
        }
    }
}