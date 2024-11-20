using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class StarShell : Shell
    {
        [SerializeField]
        private SpriteRenderer _view;
        
        [SerializeField]
        private SpriteRenderer _line1;
        
        [SerializeField]
        private SpriteRenderer _line2;

        private IEnumerator Start()
        {
            _view.color = _view.color.SetA(0);
            var changeAlphaCommand = new ChangeAlphaCommand(_view, 1, 1);
            yield return changeAlphaCommand.Await();
        }

        public void StartMove()
        {
            StartCoroutine(AwaitMove());
        }

        private IEnumerator AwaitMove()
        {
            _line1.gameObject.SetActive(true);
            _line2.gameObject.SetActive(true);

            var direction = (GameData.HeartController.transform.position - transform.position).normalized;

            _line1.transform.position -= direction * 0.04f;
            _line2.transform.position -= direction * 2 * 0.04f;
            
            while (true)
            {
                transform.position += direction * Time.deltaTime;
                yield return null;
            }
        }
    }
}