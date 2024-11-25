using System.Collections;
using UnityEngine;

namespace Game
{
    public class ZootopiaCutscene : BaseCutscene
    {
        [SerializeField]
        private SpriteRenderer _firamir;

        [SerializeField]
        private GameObject _black;
        
        protected override IEnumerator AwaitCutscene()
        {
            var music = GameData.MusicPlayer.Clip;
            var time = GameData.MusicPlayer.GetTime();
            
            yield return AwaitDialog();

            GameData.MusicPlayer.Stop();

            _black.SetActive(true);
            _firamir.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.JumpSound);
            yield return AwaitDialog();

            yield return new WaitForSeconds(1);
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.HypnosisSound);
            _black.SetActive(false);
            
            GameData.MusicPlayer.Play(music, time);
            
            yield return AwaitDialog();
            GameData.CharacterController.enabled = true;
        }
    }
}