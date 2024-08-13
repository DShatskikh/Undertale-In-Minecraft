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
        private readonly PlaySound _sparePlaySound;
        private readonly PlaySound _levelUpPlaySound;
        private readonly AudioClip _previousSound;
        private readonly Vector2 _normalWorldCharacterPosition;
        private readonly float _speedPlacement;
        private readonly LocalizedString _winReplica;

        public ExitCommand(GameObject gameObject, PlaySound sparePlaySound, PlaySound levelUpPlaySound,
            AudioClip previousSound, Vector2 normalWorldCharacterPosition, 
            float speedPlacement, LocalizedString winReplica)
        {
            _gameObject = gameObject;
            _sparePlaySound = sparePlaySound;
            _previousSound = previousSound;
            _levelUpPlaySound = levelUpPlaySound;
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
            var enemy = GameData.EnemyData.GameObject;

            if (enemy.TryGetComponent(out EnemyDisappearanceBase disappearance))
            {
                var isEnd = false;
                disappearance.Disappearance(() => isEnd = true);
                yield return new WaitUntil(() => isEnd);
            }
            else if (enemy.GetComponent<SpriteRenderer>())
            {
                var disapperance = enemy.AddComponent<SmoothDisappearance>();
                disapperance.SetDuration(0.5f);
                _sparePlaySound.Play();
                yield return new WaitForSeconds(0.5f);
            }
            
            var characterTransform = GameData.Character.transform;

            while ((Vector2)characterTransform.position != _normalWorldCharacterPosition)
            {
                characterTransform.position = Vector2.MoveTowards(characterTransform.position, _normalWorldCharacterPosition, Time.deltaTime * _speedPlacement);
                yield return null;
            }
            
            GameData.Character.enabled = true;
            GameData.Character.GetComponent<Collider2D>().isTrigger = false;

            GameData.MusicAudioSource.clip = _previousSound;
            GameData.MusicAudioSource.Play();

            var eventParams = new Dictionary<string, string>
            {
                { "Wins", GameData.EnemyData.EnemyConfig.name }
            };
            
            YandexMetrica.Send("Wins", eventParams);
            
            _winReplica.Arguments = new List<object>() { GameData.EnemyData.EnemyConfig.WinPrize };
            GameData.Monolog.Show(new []{ _winReplica });
            EventBus.CloseMonolog += () =>
            {
                _levelUpPlaySound.Play();
                
                YandexGame.savesData.MaxHealth += GameData.EnemyData.EnemyConfig.WinPrize;
                EventBus.PlayerWin.Invoke(GameData.EnemyData.EnemyConfig);
                EventBus.PlayerWin = null;
                GameData.Saver.Save();
                GameData.TimerBeforeAdsYG.gameObject.SetActive(true);
                GameData.ToMenuButton.gameObject.SetActive(true);
            };
            
            action.Invoke();
        }
    }
}