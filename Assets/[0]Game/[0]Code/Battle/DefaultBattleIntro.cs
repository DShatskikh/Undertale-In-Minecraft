using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class DefaultBattleIntro : IBattleIntro
    {
        private readonly BlackPanel _blackPanel;
        private readonly BattleProgressBar _progressBarBar;
        private readonly HealthBar _healthBar;
        private readonly Transform _container;
        private readonly Battle _battle;

        public DefaultBattleIntro()
        {
            _battle = GameData.Battle;

            _blackPanel = _battle.BlackPanel;
            _progressBarBar = _battle.ProgressBar;
            _healthBar = _battle.HealthBar;
            _container = _battle.Container;
        }

        public IEnumerator AwaitIntro()
        {
            var progress = 0.0f;

            foreach (var companion in GameData.CompanionsManager.GetAllCompanions)
                companion.GetSpriteRenderer.sortingOrder = 21;

            _blackPanel.Reset();
            _blackPanel.Show(1, 0.75f);

            var overWorldPositionsData = _battle.SessionData.SquadOverWorldPositionsData;
            overWorldPositionsData.AddRange(_battle.SessionData.EnemiesOverWorldPositions);

            foreach (var overWorldPositionData in overWorldPositionsData)
            {
                var characterMoveEffect = new MoveQuicklyEffectCommand(overWorldPositionData.Sprite, 
                    overWorldPositionData.StartPosition, overWorldPositionData.Point.position,
                    0.5f, _container);
            
                characterMoveEffect.Execute(null);
            }
            
            GameData.CharacterController.gameObject.SetActive(false);
            
            while (progress < 1)
            {
                progress += Time.deltaTime / 0.75f;

                foreach (var overWorldPositionData in overWorldPositionsData)
                    overWorldPositionData.Transform.position = Vector2.Lerp(overWorldPositionData.StartPosition,
                        overWorldPositionData.Point.position, progress);

                yield return null;
            }
            
            GameData.CharacterController.gameObject.SetActive(true);
            
            _progressBarBar.Show();
            _healthBar.Show();
        }
    }
}