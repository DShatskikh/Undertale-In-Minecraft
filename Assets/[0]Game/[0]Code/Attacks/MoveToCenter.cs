using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class MoveToCenter : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 3;

        [SerializeField]
        private UnityEvent _event;
        
        private IEnumerator Start()
        {
            var target = GameData.Arena.transform;
            
            while (transform.position != target.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * _moveSpeed);
                yield return null;
            }
            
            _event.Invoke();
        }
    }
}