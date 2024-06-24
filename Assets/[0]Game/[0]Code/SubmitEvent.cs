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
            EventBus.OnSubmit = _event.Invoke;
        }

        private void OnDisable()
        {
            EventBus.OnSubmit -= _event.Invoke;
        }
    }
}