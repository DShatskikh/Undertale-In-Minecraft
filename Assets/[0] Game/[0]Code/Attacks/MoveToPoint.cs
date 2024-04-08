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
        private float _moveSpeed = 3;

        [SerializeField]
        private UnityEvent _event;
        
        private IEnumerator Start()
        {
            while (transform.position != _point.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, _point.position, Time.deltaTime * _moveSpeed);
                yield return null;
            }
            
            _event.Invoke();
        }
    }
}