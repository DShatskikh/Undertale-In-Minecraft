using UnityEngine;

namespace Game
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] 
        private Rigidbody2D _rigidbody;

        [SerializeField] 
        private float _speed;

        [SerializeField] 
        private float _runSpeed;
        
        public void Move(Vector2 direction, bool isRun)
        {
            _rigidbody.linearVelocity = direction * (isRun ? _runSpeed : _speed);
        }
    }
}