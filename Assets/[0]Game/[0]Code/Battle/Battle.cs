using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using YG;

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

        [SerializeField]
        private AttackBase _attackTutorial;
        
        [SerializeField]
        private LocalizedString _winReplica;

        [SerializeField]
        private BlackPanel _blackPanel;

        [SerializeField]
        private BattleMessageBox _messageBox;

        [SerializeField]
        private SelectActManager _selectActManager;
        
        private Label _healthLabel;
        private Label _enemyHealthLabel;
        private Vector2 _normalWorldCharacterPosition;
        private Coroutine _coroutine;
        private AudioClip _previousSound;
        private Vector2 _enemyStartPosition;
        private AttackBase[] _attacks;
        private int _attackIndex;
        private bool _isSecondRound;

        public BlackPanel BlackPanel => _blackPanel;

        private void OnDisable()
        {
            EventBus.Death = null;
            EventBus.Damage = null;
            
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
            GameData.CharacterController.enabled = false;
            GameData.HeartController.enabled = false;
            GameData.HeartController.transform.position = GameData.Arena.transform.position;
            _previousSound = GameData.MusicPlayer.Clip;
            GameData.TimerBeforeAdsYG.gameObject.SetActive(false);
            GameData.ToMenuButton.gameObject.SetActive(false);

            _isSecondRound = false;
            gameObject.SetActive(true);

            transform.position = Camera.main.transform.position.SetZ(0).AddY(-3.5f) +
                                                 (Vector3) GameData.EnemyData.StartBattleTrigger.Offset;
            
            GameData.EnemyData.GameObject.transform.SetParent(GameData.EnemyPoint);

            if (GameData.EnemyData.GameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.flipX = true;
            }
            
            var character = GameData.CharacterController;
            character.GetComponent<Collider2D>().isTrigger = true;
            character.View.Flip(false);
            
            YandexGame.savesData.Health = YandexGame.savesData.MaxHealth;
            EventBus.HealthChange.Invoke(YandexGame.savesData.MaxHealth, YandexGame.savesData.Health);
            
            GameData.BattleProgress = 0;
            EventBus.BattleProgressChange?.Invoke(0);
            
            _attackIndex = 0;

            EventBus.Damage += OnDamage;
            EventBus.Death += OnDeath;
            
            _attacks = GameData.EnemyData.EnemyConfig.Attacks;

            var commands = new List<CommandBase>()
            {
                new IntroCommand(_startBattlePlaySound, _speedPlacement),
                new DelayCommand(1f),
                new StartEnemyTurnCommand(),
            };
            
            GameData.CommandManager.StartCommands(commands);
        }

        public void Turn(Act act = null)
        {
            var commands = new List<CommandBase>();
            GameData.HeartController.gameObject.SetActive(true);

            if (GameData.BattleProgress < 100)
            {
                var attackPrefab = YandexGame.savesData.IsTutorialComplited ? _attacks[_attackIndex] : _attackTutorial;

                if (act != null)
                {
                    commands.Add(new DelayCommand(1f));
                    commands.Add(new MessageCommand(_messageBox, act.Reaction));
                    commands.Add(new AddProgressCommand(act.Progress));
                    commands.Add(new DelayCommand(1f));
                }

                if (!_isSecondRound && YandexGame.savesData.IsTutorialComplited && _attacks[_attackIndex].Messages != null) 
                    commands.Add(new MessageCommand(_messageBox, _attacks[_attackIndex].Messages));

                commands.Add(new EnemyAttackCommand(attackPrefab, _blackPanel));
                commands.Add(new StartCharacterTurnCommand());
            }
            else
            {
                commands.Add(new DelayCommand(1f));
                commands.Add(new ExitCommand(gameObject, _sparePlaySound, _levelUpPlaySound, _previousSound,
                    _normalWorldCharacterPosition, _speedPlacement, _winReplica));
            }
            
            GameData.CommandManager.StartCommands(commands);
            GetIndex();
        }

        public void StartCharacterTurn()
        {
            _selectActManager.Activate(true);
        }

        private void GetIndex()
        {
            if (YandexGame.savesData.IsTutorialComplited)
                _attackIndex++;
            else
                YandexGame.savesData.IsTutorialComplited = true;

            if (_attackIndex >= _attacks.Length)
            {
                _isSecondRound = true;
                _attackIndex = Random.Range(0, _attacks.Length);
            }
        }

        private void OnDeath()
        {
            StopCoroutine(_coroutine);
        }

        private void OnDamage(int value)
        {
            GameData.CharacterController.View.Damage();
        }

        [ContextMenu("Progress_100")]
        private void Progress_100()
        {
            GameData.BattleProgress = 100;
        }

        public AttackBase CreateAttack(AttackBase attackPrefab)
        {
            return Instantiate(attackPrefab.gameObject, transform).GetComponent<AttackBase>();
        }
    }
}