using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HideArenaCommand : CommandBase
    {
        private readonly BattleArena _arena;
        private readonly BlackPanel _blackPanel;

        public HideArenaCommand()
        {
            _arena = GameData.Battle.SessionData.Arena;
            _blackPanel = GameData.Battle.BlackPanel;
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

            foreach (var enemiesOverWorldPosition in GameData.Battle.SessionData.EnemiesOverWorldPositions)
                enemiesOverWorldPosition.Transform.SetParent(enemiesOverWorldPosition.Point);
            
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
            
            while (progress < 1)
            {
                progress += Time.deltaTime * 1.5f;
                
                foreach (var overWorldPositionData in GameData.Battle.SessionData.SquadOverWorldPositionsData)
                    overWorldPositionData.Transform.position = Vector2.Lerp(overWorldPositionData.Point.position.AddX(6),
                        overWorldPositionData.Point.position, progress);
                
                foreach (var overWorldPositionData in GameData.Battle.SessionData.EnemiesOverWorldPositions)
                    overWorldPositionData.Transform.position = Vector2.Lerp(overWorldPositionData.Point.position.AddX(-6),
                        overWorldPositionData.Point.position, progress);
                
                yield return null;
            }

            action?.Invoke();
        }
    }
}