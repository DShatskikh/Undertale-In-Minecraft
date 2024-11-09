using System.Collections;
using UnityEngine;

namespace Game
{
    public class TheBedAtTheBusStop : UseObject
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        public override void Use()
        {
            StartCoroutine(AwaitUse());
        }

        private IEnumerator AwaitUse()
        {
            GameData.CharacterController.enabled = false;

            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.HypnosisSound);
            
            var transparencyCommand = new ChangeAlphaCommand(_spriteRenderer, 0, 1);
            yield return transparencyCommand.Await();
            
            _spriteRenderer.gameObject.SetActive(false);
            GameData.CharacterController.enabled = true;
        }
    }
}