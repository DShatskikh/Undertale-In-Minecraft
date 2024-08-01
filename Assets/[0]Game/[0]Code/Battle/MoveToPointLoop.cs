using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class MoveToPointLoop : MonoBehaviour
    {
        [SerializeField]
        private Transform _startPoint, _endPoint;
        
        [SerializeField]
        private Transform _transform;
        
        [SerializeField]
        private float _moveSpeed = 3;

        [SerializeField]
        private float _stopDuration;
        
        private IEnumerator Start()
        {
            if (_transform == null)
                _transform = transform;
            
            while (true)
            {
                var process = 0f;

                while (process < 1)
                {
                    process += Time.deltaTime * _moveSpeed;
                    _transform.position = Vector2.Lerp(_startPoint.position, _endPoint.position, process);
                    yield return null;
                }
            
                yield return new WaitForSeconds(_stopDuration);
                process = 0f;
            
                while (process < 1)
                {
                    process += Time.deltaTime * _moveSpeed;
                    _transform.position = Vector2.Lerp(_endPoint.position, _startPoint.position, process);
                    yield return null;
                }

                yield return new WaitForSeconds(_stopDuration);
            }
        }
    }
}