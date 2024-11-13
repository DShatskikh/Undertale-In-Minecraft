using System;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Kills : MonoBehaviour
    {
        [SerializeField]
        private int _count;

        [SerializeField]
        private UnityEvent _event;
        
        private void OnEnable()
        {
            EventBus.Kill += OnKill;
        }

        private void OnDisable()
        {
            EventBus.Kill -= OnKill;
        }

        private IEnumerator Start()
        {
            yield return null;
            OnKill();
        }

        private void OnKill()
        {
            if (Lua.Run("return Variable[\"KILLS\"]").AsInt >= _count) 
                _event.Invoke();
        }
    }
}