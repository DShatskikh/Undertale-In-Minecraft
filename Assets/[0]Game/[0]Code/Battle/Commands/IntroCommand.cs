using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class IntroCommand : CommandBase
    {
        private readonly PlaySound _startBattlePlaySound;
        private readonly Transform[] _points;
        
        public IntroCommand(PlaySound startBattlePlaySound, Transform[] points)
        {
            _startBattlePlaySound = startBattlePlaySound;
            _points = points;
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
            var companions = GameData.CompanionsManager.GetAllCompanions;

            var progress = 0.0f;
            var startCharacterPosition = characterTransform.position;
            var startEnemyPosition = enemyTransform.position;
            var startPositions = new List<Vector2>();
            
            for (int i = 0; i < companions.Count; i++) 
                startPositions.Add(companions[i].transform.position);
            
            while (progress < 1)
            {
                progress += Time.deltaTime * 0.75f;
                
                characterTransform.position = Vector2.Lerp(startCharacterPosition, GameData.CharacterPoint.position, progress);
                enemyTransform.position = Vector2.Lerp(startEnemyPosition, GameData.EnemyPoint.position, progress);

                for (int i = 0; i < companions.Count; i++) 
                    companions[i].transform.position = Vector2.Lerp(startPositions[i], _points[i].position, progress);

                yield return null;
            }
            
            GameData.HeartController.enabled = true;
            action?.Invoke();
        }
    }
}