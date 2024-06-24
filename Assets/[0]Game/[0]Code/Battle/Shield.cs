using System;
using System.Collections;
using Super_Auto_Mobs;
using UnityEngine;

namespace Game
{
    public class Shield : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _view;

        [SerializeField]
        private float _delayUse;
        
        [SerializeField]
        private AudioSource _source;
        
        private Attack _attack;
        private bool _isUseCoroutine;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            _isUseCoroutine = false;
            _view.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_attack && !_isUseCoroutine)
            {
                _isUseCoroutine = true;
                
                if (_coroutine != null)
                    StopCoroutine(_coroutine);
                
                _coroutine = StartCoroutine(Use());
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Attack attack))
            {
                _attack = attack;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Attack attack))
            {
                _attack = null;
            }
        }

        private IEnumerator Use()
        {
            _source.Play();
            _view.gameObject.SetActive(true);
            yield return new WaitForSeconds(_delayUse);
            _view.gameObject.SetActive(false);
            _isUseCoroutine = false;

            GameData.BattleProgress += 1;
            
            if (GameData.BattleProgress > 100)
                GameData.BattleProgress = 100;
            
            EventBus.OnBattleProgressChange?.Invoke(GameData.BattleProgress);
        }
    }
}