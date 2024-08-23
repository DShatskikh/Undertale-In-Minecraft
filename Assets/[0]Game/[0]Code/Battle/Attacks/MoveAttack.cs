using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class MoveAttack : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 3;
        
        [SerializeField]
        private Vector2 _direction = Vector2.left;
        
        private IEnumerator Start()
        {
            while (true)
            {
                transform.Translate(_direction * Time.deltaTime * _moveSpeed);
                yield return null;
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}