using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Sequence = DG.Tweening.Sequence;

namespace Game
{
    public class ShowArenaCommand : CommandBase
    {
        private readonly BattleArena _arena;
        private Sequence _showAnimation;

        public ShowArenaCommand()
        {
            _arena = GameData.Battle.SessionData.Arena;
            _showAnimation = DOTween.Sequence();
        }

        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitShow(action));
        }

        private IEnumerator AwaitShow(UnityAction action)
        {
            foreach (var overWorldPositionData in GameData.Battle.SessionData.SquadOverWorldPositionsData)
                _showAnimation.Insert(0, overWorldPositionData.Transform.DOMove(overWorldPositionData.Point.position.AddX(-6), 0.5f));

            foreach (var overWorldPositionData in GameData.Battle.SessionData.EnemiesOverWorldPositionsData)
                _showAnimation.Insert(0, overWorldPositionData.Transform.DOMove(overWorldPositionData.Point.position.AddX(6), 0.5f));
            
            yield return _showAnimation.WaitForCompletion();

            foreach (var enemiesOverWorldPosition in GameData.Battle.SessionData.EnemiesOverWorldPositionsData)
                enemiesOverWorldPosition.Transform.SetParent(null);
            
            GameData.CharacterController.View.SetOrderInLayer(0);

            var heart = GameData.HeartController;
            heart.GetComponent<Collider2D>().enabled = false;
            heart.gameObject.SetActive(true);
            heart.transform.position = _arena.StartPoint.position.AddY(4);
            
            _arena.gameObject.SetActive(true);
            GameData.Startup.StartCoroutine(_arena.AwaitUpgradeA(1, 1));

            var heartSpriteRenderer = heart.View.GetComponent<SpriteRenderer>();
            heartSpriteRenderer.color = heartSpriteRenderer.color.SetA(0);
            
            _showAnimation = DOTween.Sequence();
            _showAnimation.Insert(0, heart.transform.DOMove(_arena.StartPoint.position, 0.5f));
            _showAnimation.Insert(0, heartSpriteRenderer.DOColor(heart.View.GetComponent<SpriteRenderer>().color.SetA(1), 0.5f));

            yield return _showAnimation.WaitForCompletion();

            _arena.ShowAdditionalObjects(true);
            heart.GetComponent<Collider2D>().enabled = true;
            
            action?.Invoke();
        }
    }
}