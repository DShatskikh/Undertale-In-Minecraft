using System.Collections;
using UnityEngine;

namespace Game
{
    public class MovementPoints : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        
        [SerializeField]
        private Transform[] _points;
        
        private IEnumerator Start()
        {
            int index = 0;
            
            while (index < _points.Length)
            {
                var point = _points[index];
                
                while (transform.position != point.position)
                {
                    transform.position =
                        Vector2.MoveTowards(transform.position, point.position, Time.deltaTime * _speed);
                    
                    yield return null;
                }

                index++;
            }
        }
    }
}