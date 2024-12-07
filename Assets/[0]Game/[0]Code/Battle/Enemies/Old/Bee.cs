using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace Game
{
    public class Bee : EnemyBase
    {
        [FormerlySerializedAs("_damageEvent")]
        [SerializeField]
        private DamageAndDeathEffect damageAndDeathEvent;

        [SerializeField]
        private SpriteRenderer _view;

        [SerializeField]
        private Animator _animator, _animatorView;

        [SerializeField]
        private MoveToPointLoop _moveToPointLoop;

        [SerializeField]
        private LocalizedString _winString;
        
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
            
            //var dialogCommand = new DialogCommand(_startReplica, null, null);
            //yield return dialogCommand.Await();

            yield return null;
            
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
                //if (damageAndDeathEvent.GetHealth - value <= 0)
                //    _moveToPointLoop.enabled = false;
                
                //yield return damageAndDeathEvent.AwaitPlayDamage(this, (int)value);
            }
            
            if (eventName == "EndBattle")
            {
                /*if (damageAndDeathEvent.GetHealth <= 0)
                {
                    yield return damageAndDeathEvent.AwaitPlayDeath(this, value);
                }
                else
                {
                    var dialogCommand = new DialogCommand(_config.EndReplicas, null, null);
                    yield return dialogCommand.Await();
                    
                    GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SpareSound);
                    var changeAlphaCommand = new ChangeAlphaCommand(_view, 0, 1);
                    yield return changeAlphaCommand.Await();

                    var monologueCommand = new MonologueCommand(_winString);
                    yield return monologueCommand.Await();
                    
                    gameObject.SetActive(false);
                }*/
                
                yield break;
            }
        }
    }
}