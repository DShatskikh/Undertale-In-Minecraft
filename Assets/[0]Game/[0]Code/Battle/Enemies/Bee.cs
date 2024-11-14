using System.Collections;
using UnityEngine;

namespace Game
{
    public class Bee : EnemyBase
    {
        [SerializeField]
        private DamageEvent _damageEvent;

        [SerializeField]
        private SpriteRenderer _view;

        [SerializeField]
        private Animator _animator, _animatorView;

        [SerializeField]
        private MoveToPointLoop _moveToPointLoop;
        
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
                
                yield return _damageEvent.AwaitEvent(_config, value);
            }
            
            if (eventName == "EndBattle")
            {
                if (_damageEvent.GetHealth <= 0)
                {
                    yield return _damageEvent.AwaitDeathEvent(_config, value);
                    yield break;   
                }

                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SpareSound);
                var changeAlphaCommand = new ChangeAlphaCommand(_view, 0, 1);
                yield return changeAlphaCommand.Await();
            }
        }
    }
}