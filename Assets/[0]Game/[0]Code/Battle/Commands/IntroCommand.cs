using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class IntroCommand : CommandBase
    {
        private readonly PlaySound _startBattlePlaySound;
        private readonly float _speed;
        
        public IntroCommand(PlaySound startBattlePlaySound, float speed)
        {
            _startBattlePlaySound = startBattlePlaySound;
            _speed = speed;
        }
        
        public override void Execute(UnityAction action)
        {
            Debug.Log("IntroCommand");
            _startBattlePlaySound.Play();
            GameData.Startup.StartCoroutine(AwaitMove(action));
        }

        private IEnumerator AwaitMove(UnityAction action)
        {
            var characterTransform = GameData.CharacterController.transform;
            var enemyTransform = GameData.EnemyData.GameObject.transform;

            while (characterTransform.position != GameData.CharacterPoint.position || enemyTransform.position != GameData.EnemyPoint.position)
            {
                characterTransform.position = Vector2.MoveTowards(characterTransform.position, GameData.CharacterPoint.position, Time.deltaTime * _speed);
                enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, GameData.EnemyPoint.position, Time.deltaTime * _speed);
                yield return null;
            }
            
            GameData.HeartController.enabled = true;
            action?.Invoke();
        }
    }
}