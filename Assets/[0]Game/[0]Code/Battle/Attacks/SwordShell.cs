using System.Collections;
using UnityEngine;

namespace Game
{
    public class SwordShell : Shell
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [Header("Back")]
        [SerializeField, Range(0, 3f)]
        private float _moveDurationBack;
        
        [SerializeField, Range(0, 10f)]
        private float _distanceBack;
        
        [Header("Forward")]
        [SerializeField, Range(0, 3f)]
        private float _moveDurationForward;
        
        [SerializeField, Range(0, 30f)]
        private float _distanceForward;
        
        [Header("Delay")]
        [SerializeField, Range(0, 3f)]
        private float _delayShow;
        
        [SerializeField, Range(0, 3f)]
        private float _delayHide;

        [SerializeField, Range(0, 0.5f)]
        private float _hideADuration = 0.25f;
        
        [SerializeField, Range(0, 1.5f)]
        private  float _showADuration = 0.5f;

        [SerializeField, Range(0, 1.5f)]
        private float _backDuration;

        private IEnumerator Start()
        {
            _spriteRenderer.color = _spriteRenderer.color.SetA(0);
            
            var changeAlphaShowCommand = new ChangeAlphaCommand(_spriteRenderer, 1, _showADuration);
            StartCoroutine(changeAlphaShowCommand.Await());

            yield return new WaitForSeconds(_delayShow + 0.5f);
            
            var moveToBackCommand = new MoveToPointCommand(transform, transform .position + (transform.right * -_distanceBack), _moveDurationBack);
            yield return moveToBackCommand.Await();

            yield return new WaitForSeconds(_backDuration);
            
            GetComponent<Collider2D>().enabled = true;
            
            var moveToForwardCommand = new MoveToPointCommand(transform, transform .position + (transform.right * _distanceForward), _moveDurationForward);
            StartCoroutine(moveToForwardCommand.Await());

            yield return new WaitForSeconds(_moveDurationForward - _delayHide - _hideADuration);
            GetComponent<Collider2D>().enabled = false;
            
            var changeAlphaHideCommand = new ChangeAlphaCommand(_spriteRenderer, 0, _hideADuration);
            yield return changeAlphaHideCommand.Await();

            Destroy(gameObject);
        }
    }
}