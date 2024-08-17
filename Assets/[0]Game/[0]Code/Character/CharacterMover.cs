using UnityEngine;

namespace Game
{
    public class CharacterMover
    {
        private const float Speed = 3;
        private const float RunSpeed = 7;
        
        private readonly CharacterModel _model;
        private readonly Rigidbody2D _rigidbody;

        public CharacterMover(CharacterModel model, Rigidbody2D rigidbody)
        {
            _model = model;
            _rigidbody = rigidbody;
        }
        
        public void Move(Vector2 direction, bool isRun)
        {
            _rigidbody.linearVelocity = direction * (isRun ? RunSpeed : Speed);
        }
    }
}