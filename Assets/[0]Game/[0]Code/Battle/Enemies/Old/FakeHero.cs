using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class FakeHero : EnemyBase
    {
        [FormerlySerializedAs("_damageEvent")]
        [SerializeField]
        private DamageAndDeathEffect damageAndDeathEvent;
        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private DialogueSystemTrigger _dialogueSystemTrigger;

        [SerializeField]
        private FakeHeroCutscene_2 _fakeHeroCutscene2;
        
        public override IEnumerator AwaitCustomEvent(string eventName, float value = 0)
        {
            if (eventName == "StartBattle")
            {
                _spriteRenderer.flipX = true;
            }

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
                    //var dialogCommand = new DialogCommand(_config.EndReplicas, null, null);
                    //yield return dialogCommand.Await();
                    
                    _dialogueSystemTrigger.OnUse();
                    
                    bool isEnd = false;
                    EventBus.CloseDialog += () => isEnd = true;
                    yield return new WaitUntil(() => isEnd);
                    
                    GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SpareSound);
                    
                    gameObject.SetActive(false);
                }*/
                
                _fakeHeroCutscene2.StartCutscene();
            }
            
            yield break;
        }
    }
}