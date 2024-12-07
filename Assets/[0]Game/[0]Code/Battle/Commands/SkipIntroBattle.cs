using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SkipIntroBattle : IBattleIntro
    {
        private readonly BlackPanel _blackPanel;
        private readonly BattleProgressBar _progressBarBar;
        private readonly HealthBar _healthBar;
        private readonly Transform _container;
        private readonly Battle _battle;

        public SkipIntroBattle()
        {
            _battle = GameData.Battle;
            
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
            _blackPanel.Show(1, 0f);

            var overWorldPositionsData = _battle.SessionData.SquadOverWorldPositionsData;
            overWorldPositionsData.AddRange(_battle.SessionData.EnemiesOverWorldPositions);

            foreach (var overWorldPositionData in overWorldPositionsData)
                overWorldPositionData.Transform.position = overWorldPositionData.Point.position;
            
            _progressBarBar.Show();
            _healthBar.Show();
            
            yield break;
        }
    }
}