using System.Collections;
using UnityEngine;

namespace Game
{
    public class Slime : EnemyBase
    {
        [SerializeField]
        private DamageEvent _damageEvent;

        [SerializeField]
        private SpriteRenderer _view;
        
        [SerializeField]
        private SlimeMove _move;
        
        public override IEnumerator AwaitCustomEvent(string eventName, float value = 0)
        {
            if (eventName == "StartBattle")
            {
                _move.StopMove();
            }

            if (eventName == "Damage")
            {
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