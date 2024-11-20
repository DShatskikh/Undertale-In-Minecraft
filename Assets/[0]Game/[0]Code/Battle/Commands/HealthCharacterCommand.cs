using System.Collections;
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
            if (YandexGame.savesData.Health != YandexGame.savesData.MaxHealth)
            {
                yield return GameData.Battle.HealthPopUpLabel.AwaitAnimation(
                    GameData.CharacterController.transform.position.AddY(1),
                    $"+{YandexGame.savesData.MaxHealth - YandexGame.savesData.Health}", 
                    Color.green, "Макс", GameData.AssetProvider.HypnosisSound);
            
                YandexGame.savesData.Health = YandexGame.savesData.MaxHealth;
                EventBus.HealthChange.Invoke(YandexGame.savesData.MaxHealth, YandexGame.savesData.Health);
            }

            action.Invoke();
        }
    }
}