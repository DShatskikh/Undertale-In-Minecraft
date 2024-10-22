using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class PuzzleLeversArrow : MonoBehaviour
    {
        private Transform _slot;
        private Vector2 _startPosition;
        private float _progress;

        private void OnEnable()
        {
            StartCoroutine(AwaitMove());
        }

        public void SetCurrentSlot(Transform slot)
        {
            _slot = slot;
            _startPosition = transform.position;
            _progress = 0;
        }

        private IEnumerator AwaitMove()
        {
            while (true)
            {
                yield return null;

                if (_slot && _progress < 1f)
                {
                    _progress += 4 * Time.deltaTime;
                    transform.position = transform.position.SetX(Mathf.Lerp(_startPosition.x, _slot.position.x, _progress));
                }
                
                print("PuzzleLeversArrow");
            }
        }
    }
}