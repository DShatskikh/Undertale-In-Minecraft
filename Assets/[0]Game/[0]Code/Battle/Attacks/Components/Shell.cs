using System.Collections;
using UnityEngine;

namespace Game
{
    public class Shell : MonoBehaviour
    {
        private bool _isActive = true;
        private Coroutine _coroutine;
        public bool IsActive => _isActive;

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            if (_isActive)
                _coroutine = StartCoroutine(AwaitActive());
        }

        private IEnumerator AwaitActive()
        {
            GetComponent<Collider2D>().enabled = false;
            yield return null;
            GetComponent<Collider2D>().enabled = true;
        }
    }
}