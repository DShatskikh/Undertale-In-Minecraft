using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class Slime : EnemyBase
    {
        [SerializeField]
        private Replica[] _startReplica;

        [SerializeField]
        private SlimeMove _move;

        [SerializeField]
        private Replica[] _replicas1, _replicas2;

        [SerializeField]
        private GameObject _hat;
        
        [SerializeField]
        private DamageEvent _damageEvent;
        
        [SerializeField]
        private Transform _endBattlePoint;

        [SerializeField]
        private GameObject _dialog;

        //private Vector3 _startPosition;

        private void Start()
        {
           // _startPosition = transform.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                StartCoroutine(AwaitStartBattle());
            }
        }

        private IEnumerator AwaitStartBattle()
        {
            _move.StopMove();
            //transform.position = _startPosition;
                
            var dialogCommand = new DialogCommand(_startReplica, null, null);
            yield return dialogCommand.Await();
                
            StartBattle();
        }

        public override IEnumerator AwaitCustomEvent(string eventName, float value = 0)
        {
            if (eventName == "StartBattle")
            {

            }

            if (eventName == "Damage")
            {
                yield return _damageEvent.AwaitEvent(this, (int)value);
            }
            
            if (eventName == "EndBattle")
            {
                //transform.position = _startPosition;
                
                if (_damageEvent.GetHealth <= 0)
                {
                    yield return _damageEvent.AwaitDeathEvent(this, value);
                    yield break;   
                }

                var dialogCommand = new DialogCommand(_replicas1, null, null);
                yield return dialogCommand.Await();

                _hat.SetActive(true);
                
                var dialogCommand2 = new DialogCommand(_replicas2, null, null);
                yield return dialogCommand2.Await();

                yield return new WaitForSeconds(0.5f);
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SpareSound);
                
                var moveToPointCommand = new MoveToPointCommand(transform, _endBattlePoint.position, 1);
                yield return moveToPointCommand.Await();
                
                //var changeAlphaCommand = new ChangeAlphaCommand(_view, 0, 1);
                //yield return changeAlphaCommand.Await();
                
                _dialog.SetActive(true);
            }
        }
    }
}