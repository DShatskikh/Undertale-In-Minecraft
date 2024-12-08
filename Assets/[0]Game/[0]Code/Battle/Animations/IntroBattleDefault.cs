using System.Collections;
using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace Game
{
    public class IntroBattleDefault : IBattleIntro
    {
        private readonly BlackPanel _blackPanel;
        private readonly BattleProgressBar _progressBarBar;
        private readonly HealthBar _healthBar;
        private readonly Transform _container;
        private readonly Battle _battle;
        private readonly Sequence _moveAnimation;

        public IntroBattleDefault()
        {
            _battle = GameData.Battle;

            _blackPanel = _battle.BlackPanel;
            _progressBarBar = _battle.ProgressBar;
            _healthBar = _battle.HealthBar;
            _container = _battle.Container;

            _moveAnimation = DOTween.Sequence();
        }

        public IEnumerator AwaitIntro()
        {
            foreach (var companion in GameData.CompanionsManager.GetAllCompanions)
                companion.GetSpriteRenderer.sortingOrder = 21;

            _blackPanel.Reset();
            _blackPanel.Show(1, 0.75f);

            var squadOverWorldPositionsData = _battle.SessionData.SquadOverWorldPositionsData;
            var enemyOverWorldPositionsData = _battle.SessionData.EnemiesOverWorldPositionsData;

            foreach (var overWorldPositionData in squadOverWorldPositionsData)
            {
                var characterMoveEffect = new MoveQuicklyEffectCommand(overWorldPositionData.Sprite, 
                    overWorldPositionData.StartPosition, overWorldPositionData.Point.position,
                    0.5f, _container, false);
            
                characterMoveEffect.Execute(null);
            }
            
            foreach (var overWorldPositionData in enemyOverWorldPositionsData)
            {
                var characterMoveEffect = new MoveQuicklyEffectCommand(overWorldPositionData.Sprite, 
                    overWorldPositionData.StartPosition, overWorldPositionData.Point.position,
                    0.5f, _container, true);
            
                characterMoveEffect.Execute(null);
            }
            
            GameData.CharacterController.gameObject.SetActive(false);

            foreach (var overWorldPositionData in squadOverWorldPositionsData)
                _moveAnimation.Insert(0, overWorldPositionData.Transform.DOMove(overWorldPositionData.Point.position, 0.75f));

            foreach (var overWorldPositionData in enemyOverWorldPositionsData)
                _moveAnimation.Insert(0, overWorldPositionData.Transform.DOMove(overWorldPositionData.Point.position, 0.75f));
            
            yield return _moveAnimation.WaitForCompletion();
            
            /*while (progress < 1)
            {
                progress += Time.deltaTime / 0.75f;

                foreach (var overWorldPositionData in overWorldPositionsData)
                    overWorldPositionData.Transform.position = Vector2.Lerp(overWorldPositionData.StartPosition,
                        overWorldPositionData.Point.position, progress);

                yield return null;
            }*/
            
            GameData.CharacterController.gameObject.SetActive(true);
            
            _progressBarBar.Show();
            _healthBar.Show();
        }
    }
}