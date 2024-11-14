using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Strip : MonoBehaviour
    {
        [SerializeField]
        private Sprite _activeSprite, _deactiveSprite;

        [SerializeField]
        private Transform _startPoint;
        
        [SerializeField]
        private Transform _endPoint;

        [SerializeField]
        private Image _view;

        [SerializeField]
        private AttackActScreen _attackActScreen;

        private Coroutine _coroutine;
        
        public void StartMove()
        {
            gameObject.SetActive(true);
            _coroutine = StartCoroutine(AwaitMove());
        }

        public Vector3 GetPositionView => _view.transform.position;

        private IEnumerator AwaitMove()
        {
            var progress = 0f;

            while (progress < 1)
            {
                progress += Time.deltaTime / 1f;
                _view.transform.position = Vector3.Lerp(_startPoint.position, _endPoint.position, progress);
                yield return null;
            }
            
            _attackActScreen.EndMove();
        }
        
        public IEnumerator AwaitStop()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            for (int i = 0; i < 2; i++)
            {
                _view.sprite = _deactiveSprite;
                yield return new WaitForSeconds(0.1f);
                _view.sprite = _activeSprite;
                yield return new WaitForSeconds(0.1f);
            }
            
            //gameObject.SetActive(false);
        }
    }
}