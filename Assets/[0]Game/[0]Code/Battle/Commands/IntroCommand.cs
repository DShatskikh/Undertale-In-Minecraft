using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class IntroCommand : CommandBase
    {
        private readonly Transform[] _points;
        private readonly BlackPanel _blackPanel;

        public IntroCommand(Transform[] points, BlackPanel blackPanel)
        {
            _points = points;
            _blackPanel = blackPanel;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.WarningSound);
            GameData.Startup.StartCoroutine(AwaitMove(action));
        }

        private IEnumerator AwaitMove(UnityAction action)
        {
            yield return GameData.EnemyData.Enemy.AwaitCustomEvent("StartBattle");
            
            var characterTransform = GameData.CharacterController.transform;
            var enemyTransform = GameData.EnemyData.Enemy.transform;
            var companions = GameData.CompanionsManager.GetAllCompanions;

            var progress = 0.0f;
            var startCharacterPosition = characterTransform.position;
            var startEnemyPosition = enemyTransform.position;
            var startPositions = new List<Vector2>();

            foreach (var companion in companions)
            {
                startPositions.Add(companion.transform.position);
                companion.GetSpriteRenderer.sortingOrder = 21;
            }

            while (progress < 1)
            {
                progress += Time.deltaTime * 0.75f;
                
                characterTransform.position = Vector2.Lerp(startCharacterPosition, GameData.CharacterPoint.position, progress);
                enemyTransform.position = Vector2.Lerp(startEnemyPosition, GameData.EnemyPoint.position, progress);

                for (int i = 0; i < companions.Count; i++) 
                    companions[i].transform.position = Vector2.Lerp(startPositions[i], _points[i].position, progress);

                yield return null;
            }

            //_blackPanel.Reset();
            yield return _blackPanel.AwaitShow(1);

            GameData.HeartController.enabled = true;
            action?.Invoke();
        }
    }
}