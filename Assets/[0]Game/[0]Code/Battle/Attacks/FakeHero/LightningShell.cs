using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class LightningShell : Shell
    {
        [SerializeField]
        private SpriteRenderer _warning;

        [SerializeField]
        private GameObject _lighting1, _lighting2;

        [SerializeField]
        private GameObject _fire;

        private IEnumerator Start()
        {
            GetComponent<Collider2D>().enabled = false;
            
            _lighting1.SetActive(false);
            _lighting2.SetActive(false);
            _fire.SetActive(false);
            
            _warning.gameObject.SetActive(true);
            
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.2f);
                _warning.color = Color.red;
                yield return new WaitForSeconds(0.2f);
                _warning.color = Color.yellow;
            }
            
            _warning.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            
            GetComponent<Collider2D>().enabled = true;
            
            for (int i = 0; i < 4; i++)
            {
                _lighting1.SetActive(true);
                _lighting2.SetActive(false);
                yield return new WaitForSeconds(0.1f);
                _lighting1.SetActive(false);
                _lighting2.SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }
            
            _lighting1.SetActive(false);
            _lighting2.SetActive(false);
            
            _fire.SetActive(true);
        }
    }
}