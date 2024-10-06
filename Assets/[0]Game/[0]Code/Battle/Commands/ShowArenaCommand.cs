using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ShowArenaCommand : CommandBase
    {
        private readonly SpriteRenderer _arena;
        private readonly BlackPanel _blackPanel;
        private readonly Vector2 _size = new(8f, 5f);

        public ShowArenaCommand(SpriteRenderer arena, BlackPanel blackPanel)
        {
            _arena = arena;
            _blackPanel = blackPanel;
        }

        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitShow(action));
        }

        private IEnumerator AwaitShow(UnityAction action)
        {
            var progress = 0.0f;
            var position = GameData.Battle.Arena.transform.position;
            
            var startPosition = GameData.CharacterController.transform.position;
            var enemyStartPosition = GameData.EnemyData.GameObject.transform.position;
            
            while (progress < 1)
            {
                progress += Time.deltaTime * 1.5f;

                GameData.CharacterController.transform.position = Vector2.Lerp(startPosition, 
                    startPosition.AddX(-6), progress);
                
                GameData.EnemyData.GameObject.transform.position = Vector2.Lerp(enemyStartPosition, 
                    enemyStartPosition.AddX(6), progress);
                yield return null;
            }
            
            GameData.EnemyData.GameObject.transform.SetParent(null);
            GameData.CharacterController.View.SetOrderInLayer(0);
            
            progress = 0.0f;
            
            GameData.HeartController.gameObject.SetActive(true);
            
            while (progress < 1)
            {
                progress += Time.deltaTime * 1.0f;
                GameData.HeartController.transform.position = Vector2.Lerp(GameData.CharacterController.transform.position.AddY(0.5f), 
                    position, progress);
                yield return null;
            }

            _blackPanel.Show(1);
            _arena.size = _size;
            
            progress = 0.0f;

            while (progress < 1)
            {
                progress += Time.deltaTime * 2f;
                //_arena.size = Vector2.Lerp(Vector2.zero, _size, progress);
                _arena.color = _arena.color.SetA(progress);
                //_arena.transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, -180), Vector3.zero, progress);
                yield return null;
            }
            
            action?.Invoke();
        }
    }
}