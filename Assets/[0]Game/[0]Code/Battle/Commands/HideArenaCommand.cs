using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HideArenaCommand : CommandBase
    {
        private readonly SpriteRenderer _arena;
        private readonly BlackPanel _blackPanel;

        public HideArenaCommand(SpriteRenderer arena, BlackPanel blackPanel)
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
            GameData.HeartController.transform.position = GameData.Battle.Arena.transform.position;
            
            var progress = 0.0f;

            while (progress < 1)
            {
                progress += Time.deltaTime * 3f;
                _arena.color = _arena.color.SetA(1 - progress);
                //_arena.size = Vector2.Lerp(Vector2.one * 3, Vector2.zero, progress);
                //_arena.transform.eulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, -180), progress);
                yield return null;
            }

            var position =  GameData.HeartController.transform.position;
            _blackPanel.Show();
            GameData.EnemyData.GameObject.transform.SetParent(GameData.EnemyPoint);
            GameData.CharacterController.View.SetOrderInLayer(11);
            
            progress = 0.0f;
            var startPosition = GameData.CharacterController.transform.position;

            while (progress < 1)
            {
                progress += Time.deltaTime * 1.0f;
                GameData.HeartController.transform.position = Vector2.Lerp(position, 
                    startPosition.AddY(0.5f).AddX(-3), progress);
                yield return null;
            }

            progress = 0.0f;
            
            var enemyStartPosition = GameData.EnemyData.GameObject.transform.position;
            
            while (progress < 1)
            {
                progress += Time.deltaTime * 1.5f;

                GameData.CharacterController.transform.position = Vector2.Lerp(startPosition, 
                    GameData.CharacterPoint.position, progress);

                GameData.EnemyData.GameObject.transform.position = Vector2.Lerp(enemyStartPosition, 
                    GameData.EnemyPoint.position, progress);
                yield return null;
            }
            

            action?.Invoke();
        }
    }
}