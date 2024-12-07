using System.Collections;
using PixelCrushers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class SlimeOld : EnemyBase
    {
        [SerializeField]
        private Replica[] _startReplica;

        [FormerlySerializedAs("_move")]
        [SerializeField]
        private SlimeMover mover;

        [SerializeField]
        private Replica[] _replicas1, _replicas2;

        [SerializeField]
        private GameObject _hat;
        
        [FormerlySerializedAs("_damageEvent")]
        [SerializeField]
        private DamageAndDeathEffect damageAndDeathEvent;
        
        [SerializeField]
        private Transform _endBattlePoint;

        [SerializeField]
        private GameObject _dialog;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                StartCoroutine(AwaitStartBattle());
            }
        }

        private IEnumerator AwaitStartBattle()
        {
            mover.StopMove(true);
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
                yield return damageAndDeathEvent.AwaitPlayDamage();
            }
            
            if (eventName == "EndBattle")
            {
                //transform.position = _startPosition;
                
                /*if (damageAndDeathEvent.GetHealth <= 0)
                {
                    yield return damageAndDeathEvent.AwaitPlayDeath();
                    yield break;   
                }*/

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

        public override void ApplyData(string s)
        {
            var data = SaveSystem.Deserialize(s, _saveData);
            _saveData ??= data;

            if (_saveData.IsDefeated)
            {
                mover.StopMove(true);
                _hat.SetActive(true);
                transform.position = _endBattlePoint.position;
                GetComponent<Collider2D>().enabled = false;
                _dialog.SetActive(true);
            }
            
            print($"Slime Defeated: {_saveData.IsDefeated}");
        }
    }
}