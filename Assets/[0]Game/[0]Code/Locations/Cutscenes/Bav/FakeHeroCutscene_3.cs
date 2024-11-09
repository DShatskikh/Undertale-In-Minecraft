using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class FakeHeroCutscene_3 : BaseCutscene
    {
        [SerializeField]
        private EnderPost[] _posts;

        [SerializeField]
        private FakeHeroCutscene_4 _nextCutscene;
        
        protected override IEnumerator AwaitCutscene()
        {
            GameData.CharacterController.enabled = false;
            yield return AwaitDialog();
            GameData.CharacterController.enabled = false;

            foreach (var post in _posts)
            {
                post.ActivateCrystal();
            }
            
            GameData.CompanionsManager.TryDeactivateCompanion("FakeHero");
            
            GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.transform;
            
            _nextCutscene.StartCutscene();
            GameData.CharacterController.enabled = true;
            
            Lua.Run("Variable[\"FakeHeroState\"] = 3");
        }
    }
}