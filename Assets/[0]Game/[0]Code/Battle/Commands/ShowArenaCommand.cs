using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ShowArenaCommand : CommandBase
    {
        private readonly BattleArena _arena;

        public ShowArenaCommand()
        {
            _arena = GameData.Battle.SessionData.Arena;
        }

        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitShow(action));
        }

        private IEnumerator AwaitShow(UnityAction action)
        {
            var progress = 0.0f;

            while (progress < 1)
            {
                progress += Time.deltaTime / 0.5f;// * 1.5f;

                foreach (var overWorldPositionData in GameData.Battle.SessionData.SquadOverWorldPositionsData)
                    overWorldPositionData.Transform.position = Vector2.Lerp(overWorldPositionData.Point.position.AddX(-6),
                        overWorldPositionData.Point.position, progress);
                
                foreach (var overWorldPositionData in GameData.Battle.SessionData.EnemiesOverWorldPositions)
                    overWorldPositionData.Transform.position = Vector2.Lerp(overWorldPositionData.Point.position.AddX(6),
                        overWorldPositionData.Point.position, progress);
                
                yield return null;
            }

            foreach (var enemiesOverWorldPosition in GameData.Battle.SessionData.EnemiesOverWorldPositions)
                enemiesOverWorldPosition.Transform.SetParent(null);
            
            GameData.CharacterController.View.SetOrderInLayer(0);
            
            progress = 0.0f;
            
            var heart = GameData.HeartController;
            heart.GetComponent<Collider2D>().enabled = false;
            heart.gameObject.SetActive(true);
            heart.transform.position = _arena.StartPoint.position.AddY(4);
            
            _arena.gameObject.SetActive(true);
            GameData.Startup.StartCoroutine(_arena.AwaitUpgradeA(1, 1));

            while (progress < 1)
            {
                progress += Time.deltaTime / 0.5f;
                heart.transform.position = Vector2.Lerp( _arena.StartPoint.position.AddY(4),  _arena.StartPoint.position, progress);
                heart.View.GetComponent<SpriteRenderer>().color = heart.View.GetComponent<SpriteRenderer>().color.SetA(Mathf.Lerp(0, 1, progress));
                yield return null;
            }

            _arena.ShowAdditionalObjects(true);
            heart.GetComponent<Collider2D>().enabled = true;
            
            action?.Invoke();
        }
    }
}