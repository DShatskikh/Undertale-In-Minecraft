using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ShowArenaCommand : CommandBase
    {
        private readonly SpriteRenderer _arena;
        private readonly BlackPanel _blackPanel;

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
            
            GameData.HeartController.gameObject.SetActive(true);
            
            while (progress < 1)
            {
                progress += Time.deltaTime * 2.5f;
                GameData.HeartController.transform.position = Vector2.Lerp(GameData.CharacterController.transform.position.AddY(0.5f), 
                    position, progress);
                yield return null;
            }

            progress = 0.0f;

            while (progress < 1)
            {
                progress += Time.deltaTime * 2f;
                _arena.size = Vector2.Lerp(Vector2.zero, Vector2.one * 3, progress);
                _arena.transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, -180), Vector3.zero, progress);
                yield return null;
            }

            _blackPanel.Show();
            action?.Invoke();
        }
    }
}