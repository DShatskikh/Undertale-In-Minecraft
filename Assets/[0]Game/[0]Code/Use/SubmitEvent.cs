using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SubmitEvent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _event;
        
        private void OnEnable()
        {
            EventBus.Submit = _event.Invoke;
        }

        private void OnDisable()
        {
            EventBus.Submit -= _event.Invoke;
        }
    }
}