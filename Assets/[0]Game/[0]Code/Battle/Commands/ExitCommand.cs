using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using YG;

namespace Game
{
    public class ExitCommand : CommandBase
    {
        private readonly GameObject _gameObject;
        private readonly AudioClip _previousSound;
        private readonly Vector2 _normalWorldCharacterPosition;
        private readonly float _speedPlacement;
        private readonly LocalizedString _winReplica;

        public ExitCommand(GameObject gameObject, AudioClip previousSound, Vector2 normalWorldCharacterPosition, 
            float speedPlacement, LocalizedString winReplica)
        {
            _gameObject = gameObject;
            _previousSound = previousSound;
            _normalWorldCharacterPosition = normalWorldCharacterPosition;
            _speedPlacement = speedPlacement;
            _winReplica = winReplica;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitExit(action));
        }

        private IEnumerator AwaitExit(UnityAction action)
        {
            yield return GameData.Battle.BlackPanel.AwaitHide();
            
            GameData.EnemyData.Enemy.transform.SetParent(null);
            _gameObject.SetActive(false);
            
            GameData.CharacterController.View.SetOrderInLayer(0);
            GameData.CompanionsManager.SetMove(true);
            
            var characterTransform = GameData.CharacterController.transform;
            var enemyTransform = GameData.EnemyData.Enemy.transform;
            var startCharacterPosition = characterTransform.position;
            var startEnemyPosition = enemyTransform.position;
            
            var progress = 0f;
            
            while (progress < 1)
            {
                progress += Time.deltaTime * 0.75f;
                //characterTransform.position = Vector2.MoveTowards(characterTransform.position, _normalWorldCharacterPosition, Time.deltaTime * _speedPlacement);
                characterTransform.position = Vector2.Lerp(startCharacterPosition, _normalWorldCharacterPosition, progress);
                enemyTransform.position = Vector2.Lerp(startEnemyPosition, GameData.Battle.StartEnemyPosition, progress);
                yield return null;
            }

            foreach (var companion in GameData.CompanionsManager.GetAllCompanions)
            {
                companion.GetSpriteRenderer.sortingOrder = 0;
            }

            yield return GameData.EnemyData.Enemy.AwaitCustomEvent("EndBattle");
            
            GameData.CharacterController.enabled = true;
            GameData.CharacterController.GetComponent<Collider2D>().isTrigger = false;
            GameData.MusicPlayer.Play(_previousSound);

            var eventParams = new Dictionary<string, string>
            {
                { "Wins", GameData.EnemyData.EnemyConfig.name }
            };
            
            YandexMetrica.Send("Wins", eventParams);

            /*_winReplica.Arguments = new List<object>() { GameData.EnemyData.EnemyConfig.WinPrize };
            GameData.Monolog.Show(new []{ _winReplica });
            EventBus.CloseMonolog += () =>
            {
                if (!YandexGame.savesData.IsCheat)
                    YandexGame.savesData.MaxHealth += GameData.EnemyData.EnemyConfig.WinPrize;
                */
                
            //};
            
            EventBus.PlayerWin?.Invoke(GameData.EnemyData.EnemyConfig);
            EventBus.PlayerWin = null;
            GameData.Saver.IsSavingPosition = true;
            GameData.Saver.Save();
            GameData.InputManager.Show();

            action.Invoke();
        }
    }
}