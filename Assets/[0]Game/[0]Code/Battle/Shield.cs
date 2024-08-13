using System.Collections;
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
        
        private Shell _shell;
        private bool _isUseCoroutine;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            _isUseCoroutine = false;
            _view.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_shell && !_isUseCoroutine)
            {
                _isUseCoroutine = true;
                
                if (_coroutine != null)
                    StopCoroutine(_coroutine);
                
                _coroutine = StartCoroutine(Use());
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Shell shell))
            {
                _shell = shell;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Shell shell))
            {
                _shell = null;
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
            
            EventBus.BattleProgressChange?.Invoke(GameData.BattleProgress);
        }
    }
}