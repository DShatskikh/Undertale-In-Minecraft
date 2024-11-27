using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class FakeHeroCutscene_5 : BaseCutscene
    {
        [SerializeField]
        private EnemyBase _dragon;

        [SerializeField]
        private DamageEvent _damageEvent;

        [SerializeField]
        private FakeHeroCutscene_6 _heroCutscene6;
        
        protected override IEnumerator AwaitCutscene()
        {
            if (SaveLoadManager.GetData<DamageEvent.Data>("FakeHero_Dead").IsDead)
            {
                _heroCutscene6.StartCutscene();
            }
            else
            {
                yield return AwaitDialog();
                GameData.CharacterController.enabled = false;
                yield return new WaitForSeconds(1);
                _dragon.StartBattle();
            }

            Lua.Run("Variable[\"FakeHeroState\"] = 5");
        }
    }
}