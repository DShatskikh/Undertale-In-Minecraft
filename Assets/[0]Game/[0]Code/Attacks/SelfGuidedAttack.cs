using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class SelfGuidedAttack : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed;
        
        [SerializeField]
        private float _moveSpeed;
        
        [SerializeField]
        private float _delay;
        
        [SerializeField]
        private float _tolerance;
        
        [SerializeField]
        private Rigidbody2D _rigidbody;
        
        private Vector2 _target;

        private IEnumerator Start()
        {
            var direction = ((Vector2) GameData.Heart.transform.position - (Vector2)transform.position).normalized;
            float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;
            transform.Rotate(0, 0, rotationZ + 90);

            while (Math.Abs(transform.rotation.eulerAngles.z - rotationZ) > _tolerance)
            {
                _rigidbody.MoveRotation(Mathf.LerpAngle( _rigidbody.rotation, rotationZ, Time.deltaTime * _rotationSpeed));
                yield return null;
            }

            yield return new WaitForSeconds(_delay);
            
            while (true)
            {
                transform.Translate(Vector2.left * Time.deltaTime * _moveSpeed);
                yield return null;
            }
        }
    }
}