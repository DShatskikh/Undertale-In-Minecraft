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
                _arena.size = Vector2.Lerp(Vector2.one * 3, Vector2.zero, progress);
                _arena.transform.eulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, -180), progress);
                yield return null;
            }
            
            progress = 0.0f;
            var position =  GameData.HeartController.transform.position;
            
            while (progress < 1)
            {
                progress += Time.deltaTime * 4f;
                GameData.HeartController.transform.position = Vector2.Lerp(position, 
                    GameData.CharacterController.transform.position.AddY(0.5f), progress);
                yield return null;
            }
            
            _blackPanel.Show();
            action?.Invoke();
        }
    }
}