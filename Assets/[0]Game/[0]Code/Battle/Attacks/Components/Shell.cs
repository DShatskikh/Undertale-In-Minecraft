using System.Collections;
using UnityEngine;

namespace Game
{
    public class Shell : MonoBehaviour
    {
        private bool _isActive = true;
        private Coroutine _activeCoroutine;
        public bool IsActive => _isActive;

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
            
            if (_activeCoroutine != null)
                StopCoroutine(_activeCoroutine);

            if (_isActive)
                _activeCoroutine = StartCoroutine(AwaitActive());
        }

        private IEnumerator AwaitActive()
        {
            GetComponent<Collider2D>().enabled = false;
            yield return null;
            GetComponent<Collider2D>().enabled = true;
        }
    }
}