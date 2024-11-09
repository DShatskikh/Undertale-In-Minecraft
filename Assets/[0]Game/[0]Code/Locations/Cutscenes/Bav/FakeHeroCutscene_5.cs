using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class FakeHeroCutscene_5 : BaseCutscene
    {
        [SerializeField]
        private StartBattleTrigger _startBattleTrigger;
        
        protected override IEnumerator AwaitCutscene()
        {
            yield return AwaitDialog();
            GameData.CharacterController.enabled = false;
            yield return new WaitForSeconds(1);
            _startBattleTrigger.StartBattle();
            
            Lua.Run("Variable[\"FakeHeroState\"] = 5");
        }
    }
}