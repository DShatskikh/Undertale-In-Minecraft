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

        private void Update()
        {
            float difference = _endPoint.position.x - _startPoint.position.x;
            float progress = (_startPoint.position.x + GameData.CharacterController.transform.position.x) / _endPoint.position.x;
            _moon.transform.position = _moon.transform.position.SetX(Mathf.Lerp(_startPoint.position.x, _endPoint.position.x, progress * _parallaxEffect));
        }
    }
}