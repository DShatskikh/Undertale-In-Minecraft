using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Game
{
    public class HealthCharacterCommand : AwaitCommand
    {
        public HealthCharacterCommand()
        {
            
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.CoroutineRunner.StartCoroutine(AwaitExecute(action));
        }

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            var maxHealth = Lua.Run("return Variable[\"MaxHealth\"]").AsInt;
            
            if (GameData.HeartController.Health != maxHealth)
            {
                yield return GameData.Battle.HealthPopUpLabel.AwaitAnimation(
                    GameData.CharacterController.transform.position.AddY(1),
                    $"+{maxHealth - GameData.HeartController.Health}", 
                    Color.green, "Макс", GameData.AssetProvider.HypnosisSound);
            
                GameData.HeartController.Health = maxHealth;
                EventBus.HealthChange.Invoke(maxHealth, GameData.HeartController.Health);
            }

            action.Invoke();
        }
    }
}