using UnityEngine;

namespace Game
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] 
        private Rigidbody2D _rigidbody;

        [SerializeField] 
        private float _speed;

        public void Move(Vector2 direction)
        {
            _rigidbody.velocity = direction * _speed;
        }
    }
}