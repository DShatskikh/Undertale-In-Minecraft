using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] 
        private float _duration = 1;

        [SerializeField]
        private UnityEvent _event;

        private Coroutine _coroutine;

        private void OnEnable()
        {
            Use();
        }

        public void Use()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = GameData.CinemachineConfiner.StartCoroutine(TimerProcess());
        }
        
        private IEnumerator TimerProcess()
        {
            yield return new WaitForSeconds(_duration);
            _event.Invoke();
        }
    }
}