using System.Collections;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class Dragon : EnemyBase
    {
        [SerializeField]
        private DamageEvent _damageEvent;
        
        [SerializeField]
        private LocalizedString _winString;

        [SerializeField]
        private FakeHeroCutscene_6 _fakeHeroCutscene6;
        
        public override void StartBattle()
        {
            gameObject.SetActive(true);
            base.StartBattle();
        }

        public override IEnumerator AwaitCustomEvent(string eventName, float value = 0)
        {
            if (eventName == "Damage")
            {
                yield return _damageEvent.AwaitEvent(this, (int)value);
            }
            
            if (eventName == "EndBattle")
            {
                if (_damageEvent.GetHealth <= 0)
                {
                    yield return _damageEvent.AwaitDeathEvent(this, value);
                    gameObject.SetActive(false);
                }
                else
                {
                    var dialogCommand = new DialogCommand(_config.EndReplicas, null, null);
                    yield return dialogCommand.Await();

                    GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SpareSound);
                    
                    var monologueCommand = new MonologueCommand(_winString);
                    yield return monologueCommand.Await();
                    
                    gameObject.SetActive(false);
                }

                _fakeHeroCutscene6.StartCutscene();
            }
        }
    }
}