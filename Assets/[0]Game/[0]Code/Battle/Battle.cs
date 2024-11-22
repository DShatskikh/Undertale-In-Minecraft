using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using YG;
using Random = UnityEngine.Random;

namespace Game
{
    public class Battle : MonoBehaviour
    {
        [SerializeField]
        private AttackBase _attackTutorial;

        [SerializeField]
        private BlackPanel _blackPanel;

        [SerializeField]
        private BattleMessageBox _enemyMessageBox;

        [SerializeField]
        private BattleMessageBox _messageBox;
        
        [SerializeField]
        private SelectActManager _selectActManager;

        [SerializeField]
        private Transform[] _points;

        [Header("Variables")]
        [SerializeField]
        private float _speedPlacement;

        [SerializeField]
        private LocalizedString _winReplica;

        [SerializeField]
        private LocalizedString _winReplicaCheat;

        [SerializeField]
        private PopUpLabel _addProgressLabel;

        [SerializeField]
        private PopUpLabel _healthPopUpLabel;
        
        [SerializeField]
        private AddProgressData _addProgressData;

        [SerializeField]
        private Transform _actScreenContainer;

        private Label _healthLabel;
        private Label _enemyHealthLabel;
        private Vector2 _normalWorldCharacterPosition;
        private Coroutine _coroutine;
        private AudioClip _previousMusic;
        private Vector2 _enemyStartPosition;
        private AttackBase[] _attacks;
        private int _turnNumber;
        private BattleArena _arena;
        private Vector2 _startEnemyPosition;

        public BlackPanel BlackPanel => _blackPanel;
        public GameObject Arena => _arena.gameObject;
        public SelectActManager SelectActManager => _selectActManager;
        public Transform ActScreenContainer => _actScreenContainer;
        public BattleMessageBox EnemyMessageBox => _enemyMessageBox;
        public BattleMessageBox MessageBox => _messageBox;
        public PopUpLabel AddProgressLabel => _addProgressLabel;
        public PopUpLabel HealthPopUpLabel => _healthPopUpLabel;
        public AddProgressData AddProgressData => _addProgressData;
        public Vector2 StartEnemyPosition => _startEnemyPosition;
        public AudioClip BattleMusic;
        public AudioClip SelectMusic;

        public int? AddProgress = null;
        public float MusicStopTime;

        private void OnDisable()
        {
            EventBus.Death = null;
            EventBus.Damage = null;
        }

        public void StartBattle()
        {
            _startEnemyPosition = GameData.EnemyData.Enemy.transform.position;
            
            _normalWorldCharacterPosition = GameData.CharacterController.transform.position;
            GameData.CharacterController.enabled = false;
            GameData.HeartController.enabled = false;
            _arena = Instantiate(GameData.EnemyData.EnemyConfig.Arena, transform);
            GameData.HeartController.transform.position = _arena.transform.position;
            _previousMusic = GameData.MusicPlayer.Clip;
            GameData.Saver.IsSavingPosition = false;
            GameData.InputManager.Show();
            GameData.CompanionsManager.SetMove(false);
            
            gameObject.SetActive(true);

            transform.position = Camera.main.transform.position.SetZ(0).AddY(-3.5f);
            
            GameData.EnemyData.Enemy.transform.SetParent(GameData.EnemyPoint);

            var character = GameData.CharacterController;
            character.GetComponent<Collider2D>().isTrigger = true;
            character.View.Flip(false);
            character.View.SetOrderInLayer(11);
            
            GameData.HeartController.gameObject.SetActive(false);

            YandexGame.savesData.Health = YandexGame.savesData.MaxHealth;
            EventBus.HealthChange.Invoke(YandexGame.savesData.MaxHealth, YandexGame.savesData.Health);
            
            GameData.BattleProgress = 0;
            EventBus.BattleProgressChange?.Invoke(0);
            
            _turnNumber = 0;

            EventBus.Damage += OnDamage;
            EventBus.Death += OnDeath;
            
            _attacks = GameData.EnemyData.EnemyConfig.Attacks;

            var commands = new List<CommandBase>()
            {
                new IntroCommand(_points, _blackPanel),
                //new SkipIntroCommand(_points),
                //new DelayCommand(1f),
                new StartEnemyTurnCommand(),
            };
            
            GameData.CommandManager.StartCommands(commands);
        }

        public void Turn(BaseActConfig act = null)
        {
            GameData.MusicPlayer.Play(GameData.EnemyData.EnemyConfig.Theme 
                ? GameData.EnemyData.EnemyConfig.Theme : GameData.Battle.BattleMusic, MusicStopTime);
            
            var commands = new List<CommandBase>();

            /*if (act != null)
            {
                commands.Add(new MessageCommand(_messageBox, act.Description));
                commands.Add(new MessageCommand(_enemyMessageBox, act.Reaction));
                commands.Add(new AddProgressCommand(act.Progress, _addProgressLabel, _addProgressData));
            }*/
            
            commands.Add(new CheckEndBattleCommand());
            
            //if (!_isSecondRound && YandexGame.savesData.IsTutorialComplited && _attacks[_attackIndex].Messages != null && _attacks[_attackIndex].Messages.Length != 0) 
            //    commands.Add(new MessageCommand(_enemyMessageBox, _attacks[_attackIndex].Messages));

            commands.Add(new ShowArenaCommand(_arena));
            //commands.Add(new DelayCommand(0.5f));
            //commands.Add(new DelayCommand(1.5f));
            
            //if (!YandexGame.savesData.IsTutorialComplited)
            //    commands.Add(new EnemyAttackCommand(_attackTutorial, _blackPanel, _arena.gameObject)); 
            
            commands.Add(new EnemyAttackCommand(_attacks[GetIndex()], _blackPanel, _arena.gameObject));
            commands.Add(new CheckEndBattleCommand());
            commands.Add(new HideArenaCommand(_arena, _blackPanel));
            commands.Add(new HealthCharacterCommand());
            
            if (GameData.EnemyData.EnemyConfig.BattleReplicas.Length != 0)
            {
                var replica = _turnNumber < GameData.EnemyData.EnemyConfig.BattleReplicas.Length
                    ? GameData.EnemyData.EnemyConfig.BattleReplicas[_turnNumber]
                    : GameData.EnemyData.EnemyConfig.BattleReplicas[^1];
            
                commands.Add(new MessageCommand(GameData.Battle.EnemyMessageBox, replica));
            }
            
            commands.Add(new StartCharacterTurnCommand());

            GameData.CommandManager.StartCommands(commands);
            _turnNumber++;
        }

        public void EndBattle()
        {
            Destroy(_arena.gameObject);
            
            var winReplica = YandexGame.savesData.IsCheat ? _winReplicaCheat : _winReplica;
            
            var commands = new List<CommandBase>();
            
            commands.Add(new DelayCommand(1f));
            //commands.Add(new MessageCommand(_enemyMessageBox, GameData.EnemyData.EnemyConfig.EndReplicas));
            commands.Add(new ExitCommand(gameObject, _previousMusic, _normalWorldCharacterPosition, 
                _speedPlacement, winReplica));
            
            GameData.CommandManager.StartCommands(commands);
        }

        public void StartCharacterTurn()
        {
            //GameData.CharacterController.View.Dance();
            _selectActManager.Activate(true);
        }

        private int GetIndex()
        {
            if (_turnNumber >= _attacks.Length)
                return Random.Range(0, _attacks.Length);
            
            return _turnNumber;
        }

        private void OnDeath()
        {
            if (_coroutine != null)
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