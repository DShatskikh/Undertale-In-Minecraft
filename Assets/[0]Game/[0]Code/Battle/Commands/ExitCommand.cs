using System.Collections;
using System.Collections.Generic;
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
            _gameObject.SetActive(false);
            var enemy = GameData.EnemyData.Enemy;

            if (enemy.TryGetComponent(out EnemyDisappearanceBase disappearance))
            {
                var isEnd = false;
                disappearance.Disappearance(() => isEnd = true);
                yield return new WaitUntil(() => isEnd);
            }
            else if (enemy.GetComponent<SpriteRenderer>())
            {
                var disapperance = enemy.gameObject.AddComponent<SmoothDisappearance>();
                disapperance.SetDuration(0.5f);
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SpareSound);
                yield return new WaitForSeconds(0.5f);
            }
            
            var characterTransform = GameData.CharacterController.transform;

            while ((Vector2)characterTransform.position != _normalWorldCharacterPosition)
            {
                characterTransform.position = Vector2.MoveTowards(characterTransform.position, _normalWorldCharacterPosition, Time.deltaTime * _speedPlacement);
                yield return null;
            }
            
            GameData.CharacterController.enabled = true;
            GameData.CharacterController.GetComponent<Collider2D>().isTrigger = false;
            GameData.MusicPlayer.Play(_previousSound);

            var eventParams = new Dictionary<string, string>
            {
                { "Wins", GameData.EnemyData.EnemyConfig.name }
            };
            
            YandexMetrica.Send("Wins", eventParams);
            
            _winReplica.Arguments = new List<object>() { GameData.EnemyData.EnemyConfig.WinPrize };
            GameData.Monolog.Show(new []{ _winReplica });
            EventBus.CloseMonolog += () =>
            {
                if (!YandexGame.savesData.IsCheat)
                    YandexGame.savesData.MaxHealth += GameData.EnemyData.EnemyConfig.WinPrize;
                
                EventBus.PlayerWin.Invoke(GameData.EnemyData.EnemyConfig);
                EventBus.PlayerWin = null;
                GameData.Saver.IsSavingPosition = true;
                GameData.Saver.Save();
                GameData.InputManager.Show();
            };
            
            action.Invoke();
        }
    }
}