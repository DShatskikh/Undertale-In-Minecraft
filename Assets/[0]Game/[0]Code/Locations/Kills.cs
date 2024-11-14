using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{
    public class Kills : MonoBehaviour
    {
        [SerializeField]
        private int _count;

        [FormerlySerializedAs("_event")]
        [SerializeField]
        private UnityEvent _trueEvent;
        
        [SerializeField]
        private UnityEvent _falseEvent;
        
        private void OnEnable()
        {
            EventBus.Kill += OnKill;
        }

        private void OnDisable()
        {
            EventBus.Kill -= OnKill;
        }

        private void Start()
        {
            OnKill();
        }

        private void OnKill()
        {
            if (Lua.Run("return Variable[\"KILLS\"]").AsInt >= _count) 
                _trueEvent.Invoke();
            else
                _falseEvent.Invoke();
        }
    }
}