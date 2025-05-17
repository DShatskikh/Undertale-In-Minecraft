using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class MoveToHeart : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 3;

        [SerializeField]
        private UnityEvent _event;
        
        private IEnumerator Start()
        {
            var point = GameData.Heart.transform;
            
            while (transform.position != point.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, point.position, Time.deltaTime * _moveSpeed);
                yield return null;
            }
            
            _event.Invoke();
        }
    }
}