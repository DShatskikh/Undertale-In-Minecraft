using System.Collections;
using UnityEngine;

namespace Game
{
    public class IntroBattleSkip : IBattleIntro
    {
        private readonly BlackPanel _blackPanel;
        private readonly BattleProgressBar _progressBarBar;
        private readonly HealthBar _healthBar;
        private readonly Transform _container;
        private readonly Battle _battle;

        public IntroBattleSkip()
        {
            _battle = GameData.Battle;
            
            _progressBarBar = _battle.ProgressBar;
            _healthBar = _battle.HealthBar;
            _container = _battle.Container;
        }

        public IEnumerator AwaitIntro()
        {
            foreach (var companion in GameData.CompanionsManager.GetAllCompanions)
                companion.GetSpriteRenderer.sortingOrder = 21;

            _blackPanel.Reset();
            _blackPanel.Show(1, 0f);

            var overWorldPositionsData = _battle.SessionData.SquadOverWorldPositionsData;
            overWorldPositionsData.AddRange(_battle.SessionData.EnemiesOverWorldPositionsData);

            foreach (var overWorldPositionData in overWorldPositionsData)
                overWorldPositionData.Transform.position = overWorldPositionData.Point.position;
            
            _progressBarBar.Show();
            _healthBar.Show();
            
            yield break;
        }
    }
}