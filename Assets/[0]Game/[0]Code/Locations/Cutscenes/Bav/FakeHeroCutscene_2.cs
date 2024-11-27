using System.Collections;
using PixelCrushers;
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

        [SerializeField]
        private DamageEvent _fakeHeroDamage;

        protected override IEnumerator AwaitCutscene()
        {
            GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.transform;

            if (SaveLoadManager.GetData<DamageEvent.Data>("FakeHero_Dead").IsDead || _fakeHeroDamage.GetHealth <= 0)
            {
                _puzzleEnderCrystalsManager.gameObject.SetActive(true);
            }
            else
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

                Lua.Run("Variable[\"FakeHeroState\"] = 2");
            }
            
            GameData.CharacterController.enabled = true;
        }
    }
}