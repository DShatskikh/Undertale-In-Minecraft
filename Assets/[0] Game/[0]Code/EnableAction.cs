using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EnableAction : MonoBehaviour
    {
        [SerializeField]
         private UnityEvent _event;

         private Coroutine _coroutine;

         private void OnEnable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(ActivationEvent());
        }

        private IEnumerator ActivationEvent()
        {
            yield return null;
            _event.Invoke();
        }
    }
}