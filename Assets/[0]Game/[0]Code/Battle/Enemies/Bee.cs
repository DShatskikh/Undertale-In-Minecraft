using System.Collections;
using UnityEngine;

namespace Game
{
    public class Bee : EnemyBase
    {
        [SerializeField]
        private Replica[] _startReplica;
        
        [SerializeField]
        private DamageEvent _damageEvent;

        [SerializeField]
        private SpriteRenderer _view;

        [SerializeField]
        private Animator _animator, _animatorView;

        [SerializeField]
        private MoveToPointLoop _moveToPointLoop;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                StartCoroutine(AwaitStartBattle());
            }
        }
        
        private IEnumerator AwaitStartBattle()
        {
            _moveToPointLoop.enabled = false;
            //_move.StopMove();
            //transform.position = _startPosition;

            _view.flipX = true;
            
            var dialogCommand = new DialogCommand(_startReplica, null, null);
            yield return dialogCommand.Await();
                
            StartBattle();
        }
        
        public override IEnumerator AwaitCustomEvent(string eventName, float value = 0)
        {
            if (eventName == "StartBattle")
            {
                _animator.enabled = false;
                //_animatorView.enabled = false;
                _view.flipX = true;
                _view.transform.localPosition = Vector3.zero;
                _moveToPointLoop.enabled = true;
            }
            
            if (eventName == "Damage")
            {
                if (_damageEvent.GetHealth - value <= 0)
                    _moveToPointLoop.enabled = false;
                
                yield return _damageEvent.AwaitEvent(this, (int)value);
            }
            
            if (eventName == "EndBattle")
            {
                if (_damageEvent.GetHealth <= 0)
                {
                    yield return _damageEvent.AwaitDeathEvent(this, value);
                    yield break;   
                }

                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SpareSound);
                var changeAlphaCommand = new ChangeAlphaCommand(_view, 0, 1);
                yield return changeAlphaCommand.Await();
            }
        }
    }
}