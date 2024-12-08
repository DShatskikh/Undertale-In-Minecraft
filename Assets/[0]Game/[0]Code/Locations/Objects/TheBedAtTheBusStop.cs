using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class TheBedAtTheBusStop : UseObject
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private BedTeleport _bedTeleport;
        
        public override void Use()
        {
            StartCoroutine(AwaitUse());
        }

        private IEnumerator AwaitUse()
        {
            if (Lua.IsTrue("Variable[\"BlueCowState\"] < 3"))
            {
                GameData.CharacterController.enabled = false;

                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.HypnosisSound);
            
                var transparencyCommand = new ChangeAlphaCommand(_spriteRenderer, 0, 1);
                yield return transparencyCommand.Await();
            
                _spriteRenderer.gameObject.SetActive(false);
                GameData.CharacterController.enabled = true;
            }
            else
            {
                _bedTeleport.Use();
            }
        }
    }
}