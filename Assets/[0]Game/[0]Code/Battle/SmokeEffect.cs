using System;
using UnityEngine;

namespace Game
{
    public class SmokeEffect : MonoBehaviour
    {
        [SerializeField]
        private float _delayUse;

        [SerializeField]
        private Animator _animator;
        
        private float _currentUse;

        private void Update()
        {
            if (_currentUse < _delayUse)
                _currentUse += Time.deltaTime;
        }

        public void TryUse()
        {
            if (_currentUse >= _delayUse)
            {
                transform.position = GameData.Heart.transform.position;
                _animator.gameObject.SetActive(true);
                _animator.SetTrigger("Smoke");
            }
        }
    }
}