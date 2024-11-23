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
            if (_damageEvent.GetHealth > 0)
            {
                yield return AwaitDialog();
                GameData.CharacterController.enabled = false;
                yield return new WaitForSeconds(1);
                _dragon.StartBattle();
            }
            else
            {
                _heroCutscene6.StartCutscene();
            }

            Lua.Run("Variable[\"FakeHeroState\"] = 5");
        }
    }
}