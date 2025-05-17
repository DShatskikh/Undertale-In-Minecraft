using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class CharacterMoveToPoint : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 3;

        [SerializeField]
        private Transform _target;
        
        [SerializeField]
        private UnityEvent _event;

        public void Use()
        {
            StartCoroutine(AwaitMove());
        }
        
        private IEnumerator AwaitMove()
        {
            var point = GameData.Character.transform;
            
            while (point.position != _target.position)
            {
                point.position = Vector2.MoveTowards(point.position, _target.position, Time.deltaTime * _moveSpeed);
                yield return null;
            }
            
            _event.Invoke();
        }
    }
}