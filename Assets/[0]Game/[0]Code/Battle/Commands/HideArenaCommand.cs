using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HideArenaCommand : CommandBase
    {
        private readonly BattleArena _arena;
        private Sequence _showAnimation;

        public HideArenaCommand()
        {
            _arena = GameData.Battle.SessionData.Arena;
            _showAnimation = DOTween.Sequence();
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitHide(action));
        }
        
        private IEnumerator AwaitHide(UnityAction action)
        {
            yield return _arena.AwaitUpgradeA(0, 1);

            foreach (var enemiesOverWorldPosition in GameData.Battle.SessionData.EnemiesOverWorldPositionsData)
                enemiesOverWorldPosition.Transform.SetParent(enemiesOverWorldPosition.Point);
            
            GameData.CharacterController.View.SetOrderInLayer(11);

            _arena.ShowAdditionalObjects(false);
            
            var heart = GameData.HeartController;

            var heartSpriteRenderer = heart.View.GetComponent<SpriteRenderer>();
            heartSpriteRenderer.color = heartSpriteRenderer.color.SetA(1);
            
            _showAnimation = DOTween.Sequence();
            _showAnimation.Insert(0, heart.transform.DOMove(_arena.StartPoint.position.AddY(3.16f), 1f));
            _showAnimation.Insert(0, heartSpriteRenderer.DOColor(heart.View.GetComponent<SpriteRenderer>().color.SetA(0), 1f));

            yield return _showAnimation.WaitForCompletion();

            foreach (var overWorldPositionData in GameData.Battle.SessionData.SquadOverWorldPositionsData)
                _showAnimation.Insert(0, overWorldPositionData.Transform.DOMove(overWorldPositionData.Point.position, 0.5f));

            foreach (var overWorldPositionData in GameData.Battle.SessionData.EnemiesOverWorldPositionsData)
                _showAnimation.Insert(0, overWorldPositionData.Transform.DOMove(overWorldPositionData.Point.position, 0.5f));

            yield return _showAnimation.WaitForCompletion();
            action?.Invoke();
        }
    }
}