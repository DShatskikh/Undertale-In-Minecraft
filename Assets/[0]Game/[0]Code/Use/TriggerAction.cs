using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class TriggerAction : MonoBehaviour
    {
        [SerializeField] private UnityEvent _event;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character character))
            {
                _event.Invoke();   
            }
        }
    }
}