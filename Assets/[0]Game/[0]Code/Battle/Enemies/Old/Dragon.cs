using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace Game
{
    public class Dragon : EnemyBase
    {
        [FormerlySerializedAs("_damageEvent")]
        [SerializeField]
        private DamageAndDeathEffect damageAndDeathEvent;
        
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
                //yield return damageAndDeathEvent.AwaitPlayDamage(this, (int)value);
            }
            
            if (eventName == "EndBattle")
            {
                /*if (damageAndDeathEvent.GetHealth <= 0)
                {
                    yield return damageAndDeathEvent.AwaitPlayDeath(this, value);
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

                _fakeHeroCutscene6.StartCutscene();*/
                yield break;
            }
        }
    }
}