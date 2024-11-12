using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace Game
{
    public class Bench : MonoBehaviour
    {
        [SerializeField]
        private LocalizedString _sitString;
        
        [SerializeField]
        private LocalizedString _middleSitString;

        [SerializeField]
        private LocalizedString _endSitString;

        
        [SerializeField]
        private Transform _target;

        public void Use()
        {
            StartCoroutine(AwaitSit());
        }

        private IEnumerator AwaitSit()
        {
            GameData.CharacterController.GetComponent<Collider2D>().enabled = false;
            GameData.CharacterController.enabled = false;
            
            var startPoint = GameData.CharacterController.transform.position;
            
            var moveToTargetCommand = new MoveToPointCommand(GameData.CharacterController.transform, _target.position, 1);
            yield return moveToTargetCommand.Await();
            
            GameData.CharacterController.View.Sit();
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.JumpSound);
            
            yield return new WaitForSeconds(5);
            
            var monologueSitCommand = new MonologueCommand(new[] { _sitString });
            yield return monologueSitCommand.Await();
            GameData.CharacterController.enabled = false;
            
            yield return new WaitForSeconds(5);
            
            var monologueMiddleSitCommand = new MonologueCommand(new[] { _middleSitString });
            yield return monologueMiddleSitCommand.Await();
            GameData.CharacterController.enabled = false;
            
            yield return new WaitForSeconds(5);
            
            var monologueEndSitCommand = new MonologueCommand(new[] { _endSitString });
            yield return monologueEndSitCommand.Await();
            GameData.CharacterController.enabled = false;
            
            yield return new WaitForSeconds(0.5f);
            
            GameData.CharacterController.View.Reset();
            
            var moveToStartPositionCommand = new MoveToPointCommand(GameData.CharacterController.transform, startPoint, 1);
            yield return moveToStartPositionCommand.Await();
            
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.JumpSound);
            GameData.CharacterController.GetComponent<Collider2D>().enabled = true;
            GameData.CharacterController.enabled = true;
        }
    }
}