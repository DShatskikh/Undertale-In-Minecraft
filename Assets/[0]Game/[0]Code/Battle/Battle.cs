using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Super_Auto_Mobs;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Game
{
    public class Battle : MonoBehaviour
    {
        [Header("Test Setting")]
        [SerializeField]
        private bool _isSkipBattle;
        
        [SerializeField]
        private bool _isSkipIntro;
        
        [Header("Setting")]
        [SerializeField]
        private PlaySound _startBattlePlaySound;
        
        [SerializeField]
        private GameObject _tutorialAttack;

        [SerializeField]
        private SaveKeyBool _isFirstBattleKey;

        [SerializeField]
        private BattleIntro _intro;

        [SerializeField]
        private DialogueSystemTrigger _dialogueSystemTrigger;

        [SerializeField]
        private GameObject _hud;
        
        private Label _healthLabel;
        private Label _enemyHealthLabel;
        private int _attackIndex;
        private Vector2 _normalWorldCharacterPosition;
        private Coroutine _coroutine;
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
                    GameData.EnemyData.GameObject.transform.SetParent(GameData.EnemyData.StartBattleTrigger.transform);

                GameData.EnemyData.StartBattleTrigger = null;
            }
        }

        public void StartBattle()
        {
            gameObject.SetActive(true);
            transform.position = Camera.main.transform.position.SetZ(0);
            
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
            GameData.BattleProgress = 0;

            _attackIndex = 0;

            EventBus.OnDamage += OnDamage;
            EventBus.OnDeath += OnDeath;
            
            _startBattlePlaySound.Play();
            GameData.Character.View.Idle();
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(AwaitBattle());
        }
        
        private IEnumerator AwaitBattle()
        {
            if (_isSkipBattle)
                GameData.BattleProgress = 100;

            if (!_isSkipIntro)
                yield return _intro.Intro();
            else
                _intro.ChangeLocation();

            _hud.SetActive(true);
            GameData.Heart.enabled = true;
            
            var _attacks = GameData.EnemyData.EnemyConfig.Attacks;
            
            yield return new WaitForSeconds(1);
            
            while (GameData.BattleProgress < 100)
            {
                yield return new WaitForSeconds(0.5f);

                if (GameData.Saver.LoadKey(_isFirstBattleKey))
                {
                    _attack = Instantiate(_tutorialAttack, transform);
                    GameData.Saver.Save(_isFirstBattleKey, false);
                }
                else
                {
                    _attack = Instantiate(_attacks[_attackIndex], transform);
                    _attackIndex++;
                }
                
                yield return new WaitForSeconds(10);
                Destroy(_attack.gameObject);

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

            Lua.Run("Variable[\"PrizeMoney\"] = 10");
            Lua.Run("Variable[\"PrizeOther\"] = \"Ботинок\"");
            
            GameData.MusicAudioSource.Stop();
            _dialogueSystemTrigger.OnUse();
        }

        private void OnDeath()
        {
            StopCoroutine(_coroutine);
        }

        private void OnDamage(int value)
        {
            GameData.Character.View.Damage();
        }
    }
}