using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class OutroBattleDefault : IBattleOutro
    {
        private readonly Battle _battle;
        private readonly BattleProgressBar _progressBarBar;
        private readonly HealthBar _healthBar;
        private readonly CinemachineVirtualCamera _cinemachineVirtual;
        private Sequence _moveAnimation;

        public OutroBattleDefault()
        {
            _battle = GameData.Battle;
            _progressBarBar = _battle.ProgressBar;
            _healthBar = _battle.HealthBar;
            _cinemachineVirtual = _battle.CinemachineVirtual;
            _moveAnimation = DOTween.Sequence();
        }
        
        public IEnumerator AwaitOutro()
        {
            _progressBarBar.Hide();
            _healthBar.Hide();
            
            yield return GameData.Battle.BlackPanel.AwaitHide();

            _cinemachineVirtual.gameObject.SetActive(false);
            
            var sessionData = _battle.SessionData;
            
            foreach (var enemy in sessionData.EnemiesOverWorldPositionsData)
                enemy.Transform.SetParent(enemy.StartParent);

            foreach (var squadCompanion in sessionData.SquadOverWorldPositionsData)
                squadCompanion.Transform.SetParent(squadCompanion.StartParent);

            GameData.CharacterController.View.SetOrderInLayer(0);
            GameData.CompanionsManager.SetMove(true);

            _moveAnimation = DOTween.Sequence();
            
            foreach (var squadCompanion in sessionData.SquadOverWorldPositionsData)
                _moveAnimation.Insert(0, squadCompanion.Transform.DOMove(squadCompanion.StartPosition, 1));
            
            foreach (var enemy in sessionData.EnemiesOverWorldPositionsData)
                _moveAnimation.Insert(0, enemy.Transform.DOMove(enemy.StartPosition, 1));

            yield return _moveAnimation.WaitForCompletion();

            foreach (var companion in GameData.CompanionsManager.GetAllCompanions)
                companion.GetSpriteRenderer.sortingOrder = 0;

            GameData.MusicPlayer.Play(_battle.SessionData.PreviousTheme);
        }
    }
}