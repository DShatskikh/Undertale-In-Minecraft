using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{
    public class Genocide : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _trueEvent;
        
        [SerializeField]
        private UnityEvent _falseEvent;
        
        private void OnEnable()
        {
            OnKill();
            EventBus.Kill += OnKill;
        }

        private void OnDisable()
        {
            EventBus.Kill -= OnKill;
        }

        private void OnKill()
        {
            if (Lua.IsTrue("IsGenocide() == true")) 
                _trueEvent.Invoke();
            else
                _falseEvent.Invoke();
        }
    }
}