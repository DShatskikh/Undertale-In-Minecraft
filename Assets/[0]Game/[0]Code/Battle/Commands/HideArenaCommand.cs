using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HideArenaCommand : CommandBase
    {
        private readonly BattleArena _arena;
        private readonly BlackPanel _blackPanel;

        public HideArenaCommand(BattleArena arena, BlackPanel blackPanel)
        {
            _arena = arena;
            _blackPanel = blackPanel;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitHide(action));
        }
        
        private IEnumerator AwaitHide(UnityAction action)
        {
            //GameData.HeartController.transform.position = GameData.Battle.Arena.transform.position;
            
            var progress = 0.0f;

            yield return _arena.AwaitUpgradeA(0, 1);
            
            _blackPanel.Show();
            GameData.EnemyData.Enemy.transform.SetParent(GameData.EnemyPoint);
            GameData.CharacterController.View.SetOrderInLayer(11);

            _arena.ShowAdditionalObjects(false);
            
            progress = 0.0f;
            var heart = GameData.HeartController;
            var heartStartPosition =  heart.transform.localPosition;

            while (progress < 1)
            {
                progress += Time.deltaTime / 1;
                heart.transform.localPosition = Vector2.Lerp(heartStartPosition, heartStartPosition.SetY(3.16f), progress);
                heart.View.GetComponent<SpriteRenderer>().color = heart.View.GetComponent<SpriteRenderer>().color.SetA(Mathf.Lerp(1, 0, progress));
                yield return null;
            }
            
            progress = 0.0f;
            
            var startPosition = GameData.CharacterController.transform.position;
            var enemyStartPosition = GameData.EnemyData.Enemy.transform.position;
            
            var startPositions = new List<Vector2>();
            var companions = GameData.CompanionsManager.GetAllCompanions;
            
            foreach (var companion in companions) 
                startPositions.Add(companion.transform.position);
            
            while (progress < 1)
            {
                progress += Time.deltaTime * 1.5f;

                GameData.CharacterController.transform.position = Vector2.Lerp(startPosition, 
                    GameData.CharacterPoint.position, progress);

                for (int i = 0; i < companions.Count; i++) 
                    companions[i].transform.position = Vector2.Lerp(startPositions[i], startPositions[i].AddX(6), progress);
                
                GameData.EnemyData.Enemy.transform.position = Vector2.Lerp(enemyStartPosition, 
                    GameData.EnemyPoint.position, progress);
                yield return null;
            }

            action?.Invoke();
        }
    }
}