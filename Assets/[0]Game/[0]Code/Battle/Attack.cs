using System;
using UnityEngine;

namespace Game
{
    public class Attack : MonoBehaviour
    {
        public Vector2 Direction;

        private Vector2 _previousPosition;
        
        private void FixedUpdate()
        {
            Direction = ((Vector2)transform.position - _previousPosition).normalized;
            _previousPosition = transform.position;
        }
    }
}