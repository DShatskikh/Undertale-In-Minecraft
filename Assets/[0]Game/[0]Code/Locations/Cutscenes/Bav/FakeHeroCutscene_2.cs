using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class FakeHeroCutscene_2 : BaseCutscene
    {
        [SerializeField]
        private PuzzleEnderCrystalsManager _puzzleEnderCrystalsManager;
        
        [SerializeField]
        private AudioClip _musicClip;

        [SerializeField]
        private FakeHero _fake;
        
        protected override IEnumerator AwaitCutscene()
        {
            GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.transform;
            
            if (!Lua.IsTrue("IsFakeHeroDead"))
            {
                GameData.MusicPlayer.Play(_musicClip);
            
                GameData.CharacterController.enabled = false;
                yield return AwaitDialog();
                GameData.CharacterController.enabled = false;

                yield return new WaitForSeconds(1);
            
                _puzzleEnderCrystalsManager.gameObject.SetActive(true);
                
                GameData.CompanionsManager.TryActivateCompanion("FakeHero");
                GameData.CompanionsManager.GetCompanion("FakeHero").transform.position = _fake.transform.position;
                _fake.gameObject.SetActive(false);
                GameData.CharacterController.enabled = true;
            
                Lua.Run("Variable[\"FakeHeroState\"] = 2");
            }
            else
            {
                _puzzleEnderCrystalsManager.gameObject.SetActive(true);
            }
        }
    }
}