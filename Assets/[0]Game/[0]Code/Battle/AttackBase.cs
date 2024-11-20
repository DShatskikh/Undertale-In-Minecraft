using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class AttackBase : MonoBehaviour
    {
        private Coroutine _coroutine;

        private void OnDestroy()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        public void Execute(UnityAction action)
        {
            _coroutine = StartCoroutine(AwaitExecute(action));
        }

        protected abstract IEnumerator AwaitExecute(UnityAction action);
    }
}