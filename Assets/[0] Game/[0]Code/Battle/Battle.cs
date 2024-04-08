using System.Collections;
using System.Collections.Generic;
using Super_Auto_Mobs;
using UnityEngine;
using UnityEngine.UIElements;
using YG;
using Random = UnityEngine.Random;

namespace Game
{
    public class Battle : MonoBehaviour
    {
        [SerializeField] 
        private float _speedPlacement;

        [SerializeField]
        private PlaySound _startBattlePlaySound;

        [SerializeField]
        private PlaySound _levelUpPlaySound;
        
        [SerializeField]
        private PlaySound _sparePlaySound;
        
        private Label _healthLabel;
        private Label _enemyHealthLabel;
        private int _attackIndex;
        private Vector2 _normalWorldCharacterPosition;
        private Coroutine _coroutine;
        private AudioClip _previousSound;
        private Vector2 _enemyStartPosition;
        private GameObject _attack;

        private void OnDisable()
        {
            EventBus.OnDeath = null;
            EventBus.OnDamage = null;
            
            if (_attack)
                Destroy(_attack.gameObject);

            if (GameData.EnemyData != null)
            {
                if (GameData.EnemyData.GameObject != null && GameData.EnemyData.StartBattleTrigger != null)
                {
                    GameData.EnemyData.GameObject.transform.SetParent(GameData.EnemyData.StartBattleTrigger.transform);
                }
                
                GameData.EnemyData.StartBattleTrigger = null;
            }
        }

        public void StartBattle()
        {
            _previousSound = GameData.MusicAudioSource.clip;
            GameData.TimerBeforeAdsYG.gameObject.SetActive(false);
            
            gameObject.SetActive(true);

            GameData.Battle.transform.position = Camera.main.transform.position.SetZ(0).AddY(-3.5f) +
                                                 (Vector3) GameData.EnemyData.StartBattleTrigger.Offset;
            
            GameData.EnemyData.GameObject.transform.SetParent(GameData.EnemyPoint);

            if (GameData.EnemyData.GameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.flipX = true;
            }
            
            var character = GameData.Character;
            character.enabled = false;
            character.GetComponent<Collider2D>().isTrigger = true;
            character.View.Flip(false);
            
            GameData.Health = GameData.MaxHealth;
            EventBus.OnHealthChange.Invoke(GameData.MaxHealth, GameData.Health);
            
            GameData.BattleProgress = 0;
            EventBus.OnBattleProgressChange?.Invoke(0);
            
            _attackIndex = 0;

            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(AwaitBattle());
        }
        
        private IEnumerator AwaitBattle()
        {
            EventBus.OnDamage += OnDamage;
            EventBus.OnDeath += OnDeath;
            yield return Intro();

            var _attacks = GameData.EnemyData.EnemyConfig.Attacks;
            
            yield return new WaitForSeconds(1);
            
            while (GameData.BattleProgress < 100)
            {
                GameData.Arena.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                _attack = Instantiate(_attacks[_attackIndex], transform);
                yield return new WaitForSeconds(10);
                Destroy(_attack.gameObject);
                _attackIndex++;

                if (_attackIndex >= _attacks.Length)
                {
                    _attackIndex = Random.Range(0, _attacks.Length);

                    if (GameData.EnemyData.EnemyConfig.SkipAttack != null)
                    {
                        while (_attacks[_attackIndex] == GameData.EnemyData.EnemyConfig.SkipAttack)
                        {
                            _attackIndex = Random.Range(0, _attacks.Length);
                        }
                    }
                }

                GameData.BattleProgress += GameData.EnemyData.EnemyConfig.ProgressAttack;

                if (GameData.BattleProgress > 100)
                    GameData.BattleProgress = 100;
                
                EventBus.OnBattleProgressChange?.Invoke(GameData.BattleProgress);
            }
            
            yield return new WaitForSeconds(1);
            GameData.Character.StartCoroutine(Exit());
        }

        private IEnumerator Intro()
        {
            _startBattlePlaySound.Play();
            GameData.Character.View.Idle();
            
            var characterTransform = GameData.Character.transform;
            var enemyTransform = GameData.EnemyData.GameObject.transform;
            
            _enemyStartPosition = enemyTransform.position;
            _normalWorldCharacterPosition = characterTransform.position;
            
            while (characterTransform.position != GameData.CharacterPoint.position || enemyTransform.position != GameData.EnemyPoint.position)
            {
                characterTransform.position = Vector2.MoveTowards(characterTransform.position, GameData.CharacterPoint.position, Time.deltaTime * _speedPlacement);
                enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, GameData.EnemyPoint.position, Time.deltaTime * _speedPlacement);
                yield return null;
            }
        }

        private void OnDeath()
        {
            StopCoroutine(_coroutine);
        }
        
        private IEnumerator Exit()
        {
            gameObject.SetActive(false);

            var disapperance = GameData.EnemyData.GameObject.AddComponent<SmoothDisappearance>();
            disapperance.SetDuration(0.5f);
            _sparePlaySound.Play();
            yield return new WaitForSeconds(0.5f);

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
            
            GameData.Monolog.Show(new []{$"*Вы победили!\n*Ваше максимальное здоровье увеличилось\nна {GameData.EnemyData.EnemyConfig.WinPrize}"});
            EventBus.OnCloseMonolog += () =>
            {
                _levelUpPlaySound.Play();
                
                GameData.MaxHealth += GameData.EnemyData.EnemyConfig.WinPrize;
                EventBus.OnPlayerWin.Invoke(GameData.EnemyData.EnemyConfig);
                EventBus.OnPlayerWin = null;
                GameData.Saver.Save();
                GameData.TimerBeforeAdsYG.gameObject.SetActive(true);
            };
        }

        private void OnDamage(int value)
        {
            GameData.Character.View.Damage();
        }
    }
}