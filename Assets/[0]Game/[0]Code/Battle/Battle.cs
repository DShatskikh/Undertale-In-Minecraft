using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Serialization;
using YG;

namespace Game
{
    public class Battle : MonoBehaviour, IBattle
    {
        [Header("Variables")]
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

        [FormerlySerializedAs("_companionsPoints")]
        [FormerlySerializedAs("_points")]
        [SerializeField]
        private Transform[] _squadPoints;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private PopUpLabel _addProgressLabel;

        [SerializeField]
        private PopUpLabel _healthPopUpLabel;
        
        [SerializeField]
        private AddProgressData _addProgressData;

        [SerializeField]
        private Transform _actScreenContainer;

        [FormerlySerializedAs("progressBarBar")]
        [FormerlySerializedAs("_progressBar")]
        [FormerlySerializedAs("_battleProgress")]
        [SerializeField]
        private BattleProgressBar _progressBarBar;
        
        [SerializeField]
        private HealthBar _healthBar;
        
        [SerializeField]
        private Transform[] _enemiesPoints;
        
        [Header("Default Data")]
        [FormerlySerializedAs("_battleMusic")]
        [SerializeField]
        private AudioClip _battleTheme;
        
        [SerializeField]
        private AudioClip _selectTheme;
        
        private Coroutine _coroutine;
        private BattleSessionData _sessionData;

        public BlackPanel BlackPanel => _blackPanel;
        public SelectActManager SelectActManager => _selectActManager;
        public Transform ActScreenContainer => _actScreenContainer;
        public BattleMessageBox EnemyMessageBox => _enemyMessageBox;
        public BattleMessageBox MessageBox => _messageBox;
        public PopUpLabel AddProgressLabel => _addProgressLabel;
        public PopUpLabel HealthPopUpLabel => _healthPopUpLabel;
        public AddProgressData AddProgressData => _addProgressData;
        public Transform[] SquadPoints => _squadPoints;
        public BattleSessionData SessionData => _sessionData;
        public BattleProgressBar ProgressBar => _progressBarBar;
        public HealthBar HealthBar => _healthBar;
        public Transform Container => _container;

        public IEnemy SelectEnemy { get; set; }
        
        public class BattleSessionData
        {
            public IBattleController BattleController;
            public IBattleIntro Intro;
            public AudioClip PreviousTheme;
            public AudioClip BattleTheme;
            public AudioClip SelectTheme;
            public int Progress;
            public float ThemeTime;
            public List<OverWorldPositionsData> EnemiesOverWorldPositions;
            public List<OverWorldPositionsData> SquadOverWorldPositionsData;
            public BattleArena Arena;
            public IBattleOutro Outro;
            public int? AddProgress;
        }

        public struct OverWorldPositionsData
        {
            public Transform Transform;
            public Transform Point;
            public Vector2 StartPosition;
            public Transform StartParent;
            public Sprite Sprite;

            public OverWorldPositionsData(Transform transform, Transform point, Sprite sprite)
            {
                Transform = transform;
                Point = point;
                StartPosition = transform.position;
                StartParent = transform.parent;
                Sprite = sprite;
            }
        }

        private void OnDisable()
        {
            EventBus.Death = null;
            EventBus.Damage = null;
        }

        public IBattle Init()
        {
            _sessionData = new BattleSessionData()
            {
                Intro = new DefaultBattleIntro(),
                Outro = new OutroBattleDefault(),
                PreviousTheme = GameData.MusicPlayer.Clip,
                BattleTheme = _battleTheme,
                SelectTheme = _selectTheme,
                Progress = 0,
                ThemeTime = 0,
                SquadOverWorldPositionsData = GetSquadOverWorldPositionsData()
            };

            return this;
        }

        public IBattle SetIntro(IBattleIntro intro)
        {
            _sessionData.Intro = intro;
            return this;
        }

        public IBattle SetOutro(IBattleOutro outro)
        {
            _sessionData.Outro = outro;
            return this;
        }

        public IBattle SetBattleTheme(AudioClip theme)
        {
            _sessionData.BattleTheme = theme;
            return this;
        }

        public void StartBattle(IBattleController battleController)
        {
            _sessionData.BattleController = battleController;
            _sessionData.EnemiesOverWorldPositions = GetEnemiesOverWorldPositionsData();

            gameObject.SetActive(true);
            
            GameData.SaveLoadManager.IsSave = false;
            PlayBattleTheme();

            var enemies = battleController.GetEnemies();
            var character = GameData.CharacterController;
            character.enabled = false;
            character.GetComponent<Collider2D>().isTrigger = true;
            character.View.Flip(false);
            character.View.SetOrderInLayer(11);
            
            GameData.CompanionsManager.SetMove(false);
            
            _sessionData.Arena = Instantiate(battleController.GetArena(), transform);
            
            var maxHealth = Lua.Run("return Variable[\"MaxHealth\"]").AsInt;
            GameData.HeartController.Health = maxHealth;
            EventBus.HealthChange.Invoke(maxHealth, maxHealth);
            GameData.HeartController.gameObject.SetActive(false);
            GameData.HeartController.enabled = false;
            GameData.HeartController.transform.position = _sessionData.Arena.transform.position;

            GameData.InputManager.Show();
            transform.position = Camera.main.transform.position.SetZ(0).AddY(-3.5f);

            for (var index = 0; index < enemies.Length; index++)
            {
                var enemy = enemies[index];
                ((MonoBehaviour)enemy).transform.SetParent(_enemiesPoints[index]);
            }
            
            EventBus.BattleProgressChange?.Invoke(0);
            
            EventBus.Damage += OnDamage;
            EventBus.Death += OnDeath;

            StartCoroutine(AwaitStartBattle());
        }

        public void PlayerTurn()
        {
            PlayBattleTheme();
            _selectActManager.Activate(true);
        }


        public void EndBattle()
        {
            StartCoroutine(AwaitOutro());
        }

        private IEnumerator AwaitOutro()
        {
            yield return _sessionData.Outro;
            
            GameData.CharacterController.enabled = true;
            GameData.CharacterController.GetComponent<Collider2D>().isTrigger = false;

            var eventParams = new Dictionary<string, string>
            {
                { "Wins", _sessionData.BattleController.GetIndex }
            };
            
            YandexMetrica.Send("Wins", eventParams);
            
            GameData.InputManager.Show();
            GameData.SaveLoadManager.IsSave = true;
        }
        
        private IEnumerator AwaitStartBattle()
        {
            yield return _sessionData.Intro.AwaitIntro();
            yield return _sessionData.BattleController.AwaitEndIntro();
            _sessionData.BattleController.Turn();
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

        public AttackBase CreateAttack(AttackBase attackPrefab) => 
            Instantiate(attackPrefab.gameObject, transform).GetComponent<AttackBase>();

        private void PlayBattleTheme() => 
            GameData.MusicPlayer.Play(_sessionData.BattleTheme, _sessionData.ThemeTime);

        private List<OverWorldPositionsData> GetSquadOverWorldPositionsData()
        {
            var squadOverWorldPositionsData = new List<OverWorldPositionsData>
            {
                new(GameData.CharacterController.transform, SquadPoints[0], 
                    GameData.CharacterController.View.GetSprite())
            };

            var index = 1;
            
            foreach (var companion in GameData.CompanionsManager.GetAllCompanions)
            {
                squadOverWorldPositionsData.Add(new OverWorldPositionsData(companion.transform, 
                    SquadPoints[index], companion.GetSpriteRenderer.sprite));
                
                index++;
            }

            return squadOverWorldPositionsData;
        }

        private List<OverWorldPositionsData> GetEnemiesOverWorldPositionsData()
        {
            var enemiesOverWorldPositions = new List<OverWorldPositionsData>();
            var enemies = _sessionData.BattleController.GetEnemies();
            
            for (var index = 0; index < enemies.Length; index++)
            {
                var enemy = enemies[index];
                enemiesOverWorldPositions.Add(new OverWorldPositionsData(((MonoBehaviour)enemy).transform,
                    _enemiesPoints[index], enemy.GetSprite()));
            }

            return enemiesOverWorldPositions;
        }
    }
}