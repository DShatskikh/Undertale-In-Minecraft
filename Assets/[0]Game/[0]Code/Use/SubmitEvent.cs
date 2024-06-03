using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SubmitEvent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _event;

        private void Update()
        {
            if (Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(1))
            {
                OnSubmit();
            }
        }

        [ContextMenu("Submit")]
        private void OnSubmit()
        {
            print("Submit");
            _event.Invoke();
        }
    }
}