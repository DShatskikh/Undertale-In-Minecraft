using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using YG;

namespace Game
{
    public class OutroBattleDefault : IBattleOutro
    {
        private readonly Battle _battle;

        public OutroBattleDefault()
        {
            _battle = GameData.Battle;
        }

        public IEnumerator AwaitOutro()
        {
            yield return GameData.Battle.BlackPanel.AwaitHide();

            _battle.gameObject.SetActive(false);
            
            GameData.CharacterController.View.SetOrderInLayer(0);
            GameData.CompanionsManager.SetMove(true);

            var sessionData = _battle.SessionData;
            
            foreach (var enemy in sessionData.EnemiesOverWorldPositions)
                enemy.Transform.SetParent(enemy.StartParent);

            foreach (var squadCompanion in sessionData.SquadOverWorldPositionsData)
                squadCompanion.Transform.SetParent(squadCompanion.StartParent);
            
            var progress = 0f;
            
            while (progress < 1)
            {
                progress += Time.deltaTime * 0.75f;

                foreach (var squadCompanion in sessionData.SquadOverWorldPositionsData)
                    squadCompanion.Transform.position = Vector2.Lerp(squadCompanion.Point.position,
                        squadCompanion.StartPosition, progress);

                foreach (var enemy in sessionData.EnemiesOverWorldPositions)
                    enemy.Transform.position = Vector2.Lerp(enemy.Point.position, 
                        enemy.StartPosition, progress);

                yield return null;
            }

            foreach (var companion in GameData.CompanionsManager.GetAllCompanions)
                companion.GetSpriteRenderer.sortingOrder = 0;

            GameData.MusicPlayer.Play(_battle.SessionData.PreviousTheme);
        }
    }
}