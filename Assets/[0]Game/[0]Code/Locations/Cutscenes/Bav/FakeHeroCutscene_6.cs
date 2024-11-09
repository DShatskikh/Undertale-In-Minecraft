using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class FakeHeroCutscene_6 : BaseCutscene
    {
        [SerializeField]
        private GameObject _darkPortal;
        
        protected override IEnumerator AwaitCutscene()
        {
            yield return AwaitDialog();
            _darkPortal.SetActive(true);

            Lua.Run("Variable[\"FakeHeroState\"] = 6");
            GameData.CompanionsManager.TryActivateCompanion("FakeHero");
            GameData.CharacterController.enabled = true;
        }
    }
}