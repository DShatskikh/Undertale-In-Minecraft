using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class FakeHeroCutscene_5 : BaseCutscene
    {
        [SerializeField]
        private EnemyBase _dragon;

        [FormerlySerializedAs("_damageEvent")]
        [SerializeField]
        private DamageAndDeathEffect damageAndDeathEvent;

        [SerializeField]
        private FakeHeroCutscene_6 _heroCutscene6;
        
        protected override IEnumerator AwaitCutscene()
        {
            /*if (SaveLoadManager.GetData<DamageAndDeathEffect.Data>("FakeHero_Dead").IsDead)
            {
                _heroCutscene6.StartCutscene();
            }
            else
            {
                yield return AwaitDialog();
                GameData.CharacterController.enabled = false;
                yield return new WaitForSeconds(1);
                _dragon.StartBattle();
            }*/

            Lua.Run("Variable[\"FakeHeroState\"] = 5");
            
            yield break;
        }
    }
}