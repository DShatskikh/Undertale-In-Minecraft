using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class TheBedAtTheBusStop : UseObject
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private LocalizedString[] _endReplicas;

        public override void Use()
        {
            StartCoroutine(AwaitUse());
        }

        private IEnumerator AwaitUse()
        {
            GameData.CharacterController.enabled = false;

            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.HypnosisSound);
            
            var transparencyCommand = new TransparencyCommand(_spriteRenderer, 0, 1);
            yield return transparencyCommand.Await();
            
            _spriteRenderer.gameObject.SetActive(false);
            GameData.CharacterController.enabled = true;
            
            //var monologueCommand = new MonologueCommand(_endReplicas, null);
            //yield return monologueCommand.Await();
        }
    }
}