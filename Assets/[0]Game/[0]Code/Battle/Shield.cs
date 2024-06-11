using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Game
{
    public class Shield : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _view, _view2;

        [SerializeField]
        private float _delayUse;

        [SerializeField]
        private AudioSource _source;

        [SerializeField]
        private MMF_Player _addScoreFeedback;
        
        private Attack _attack;
        private bool _isUseCoroutine;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            _isUseCoroutine = false;
            ViewHide();
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
            ViewShow();
            
            if (_attack)
                GameData.Heart.Push(_attack.Direction);
            
            yield return new WaitForSeconds(_delayUse);
            ViewHide();
            _isUseCoroutine = false;

            var addProgress = 1;
            
            GameData.BattleProgress += addProgress;

            MMF_FloatingText floatingText = _addScoreFeedback.GetFeedbackOfType<MMF_FloatingText>();
            floatingText.Value = $"+{addProgress}";
            _addScoreFeedback.PlayFeedbacks(transform.position);
            
            if (GameData.BattleProgress > 100)
                GameData.BattleProgress = 100;
            
            EventBus.OnBattleProgressChange?.Invoke(GameData.BattleProgress);
        }

        private void ViewShow()
        {
            _view.color = _view.color.SetA(1);
           // _view2.gameObject.SetActive(true);
        }

        private void ViewHide()
        {
            _view.color = _view.color.SetA(30 / 255f);
            //_view2.gameObject.SetActive(false);
        }
    }
}