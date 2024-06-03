using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class OpenJournal : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _event;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
                _event.Invoke();
        }
    }
}