using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Game
{
    public class ShowArenaCommand : CommandBase
    {
        private readonly BattleArena _arena;

        public ShowArenaCommand(BattleArena arena)
        {
            _arena = arena;
        }

        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitShow(action));
        }

        private IEnumerator AwaitShow(UnityAction action)
        {
            var progress = 0.0f;
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
                    startPosition.AddX(-6), progress);

                for (int i = 0; i < companions.Count; i++) 
                    companions[i].transform.position = Vector2.Lerp(startPositions[i], startPositions[i].AddX(-6), progress);
                
                GameData.EnemyData.Enemy.transform.position = Vector2.Lerp(enemyStartPosition, 
                    enemyStartPosition.AddX(6), progress);
                yield return null;
            }
            
            GameData.EnemyData.Enemy.transform.SetParent(null);
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
                progress += Time.deltaTime / 1;
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