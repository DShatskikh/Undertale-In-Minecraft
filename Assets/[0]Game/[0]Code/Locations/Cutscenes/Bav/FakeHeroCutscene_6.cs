using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class FakeHeroCutscene_6 : BaseCutscene
    {
        [SerializeField]
        private GameObject _darkPortal;

        [SerializeField]
        private DamageEvent _fakeHero;
        
        protected override IEnumerator AwaitCutscene()
        {
            if (_fakeHero.GetHealth > 0)
                yield return AwaitDialog();
            
            _darkPortal.SetActive(true);

            Lua.Run("Variable[\"FakeHeroState\"] = 6");
            Lua.Run("Variable[\"BlueCowState\"] = 2");

            if (_fakeHero.GetHealth > 0)
                GameData.CompanionsManager.TryActivateCompanion("FakeHero");

            GameData.CharacterController.enabled = true;
        }
    }
}