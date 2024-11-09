using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class MoveToPoint : MonoBehaviour
    {
        [SerializeField]
        private Transform _point;
        
        [SerializeField]
        private Transform _transform;
        
        [SerializeField]
        private float _moveSpeed = 3;

        [SerializeField]
        private UnityEvent _event;
        
        private IEnumerator Start()
        {
            if (_transform == null)
                _transform = transform;
            
            while (_transform.position != _point.position)
            {
                _transform.position = Vector2.MoveTowards(_transform.position, _point.position, Time.deltaTime * _moveSpeed);
                yield return null;
            }
            
            _event.Invoke();
        }
    }
}