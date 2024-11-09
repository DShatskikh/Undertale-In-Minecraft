using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class FakeHeroCutscene_2 : BaseCutscene
    {
        [SerializeField]
        private PuzzleEnderCrystalsManager _puzzleEnderCrystalsManager;

        [SerializeField]
        private Transform _originalFakeHero;
        
        protected override IEnumerator AwaitCutscene()
        {
            GameData.CharacterController.enabled = false;
            yield return AwaitDialog();
            GameData.CharacterController.enabled = false;

            yield return new WaitForSeconds(1);
            
            _puzzleEnderCrystalsManager.gameObject.SetActive(true);
            
            GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.transform;
            GameData.CompanionsManager.TryActivateCompanion("FakeHero");
            GameData.CompanionsManager.GetCompanion("FakeHero").transform.position = _originalFakeHero.position;
            _originalFakeHero.gameObject.SetActive(false);
            GameData.CharacterController.enabled = true;
            
            Lua.Run("Variable[\"FakeHeroState\"] = 2");
        }
    }
}