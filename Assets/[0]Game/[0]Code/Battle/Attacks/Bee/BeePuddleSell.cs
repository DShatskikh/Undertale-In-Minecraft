using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class BeePuddleSell : Shell
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            SetActive(false);
            StartCoroutine(AwaitShow());
        }

        private IEnumerator AwaitShow()
        {
            yield return new WaitForSeconds(0.5f);
            SetActive(true);
            yield return new WaitForSeconds(4f);
            SetActive(false);
            var changeAlphaCommand = new ChangeAlphaCommand(_spriteRenderer, 0, 1);
            yield return changeAlphaCommand.Await();
            Destroy(gameObject);
        }
    }
}