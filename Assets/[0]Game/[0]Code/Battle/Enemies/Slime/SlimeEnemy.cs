using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class SlimeEnemy : MonoBehaviour, IEnemy, IDamageAndDeath
    {
        [Header("Configs")]
        [SerializeField]
        private EnemyConfig _enemyConfig;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private SlimeMover _mover;

        [SerializeField]
        private int _maxHealth;
        
        [SerializeField]
        private DamageAndDeathEffect _damageAndDeathEffect;

        [Header("Replicas")]
        [SerializeField]
        private LocalizedString _deathLocalizedString;

        private int _health;
        private IBattleController _battleController;

        public int Health => _health;
        public int MaxHealth => _maxHealth;
        
        public BaseActConfig[] GetActs => _enemyConfig.Acts;
        public EnemyConfig GetConfig => _enemyConfig;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController characterController))
            {
                GetComponent<Collider2D>().enabled = false;
                _mover.StopMove();
                _battleController.StartBattle();
            }
        }

        public void Init(IBattleController battleController)
        {
            _battleController = battleController;
            _health = _maxHealth;
            _damageAndDeathEffect.Init(this);
        }

        public IEnumerator AwaitEndIntro()
        {
            yield return _mover.AwaitPlayDown();
        }

        public IEnumerator AwaitReaction(string actName, float value)
        {
            throw new System.NotImplementedException();
        }

        public Sprite GetSprite() => 
            _spriteRenderer.sprite;

        public LocalizedString GetName() => 
            _enemyConfig.NameLocalized;
        
        public IEnumerator AwaitDamage(int damage)
        {
            _health -= damage;
            
            if (_health < (_maxHealth * 30) / 100)
                _health = 0;
            
            yield return _damageAndDeathEffect.AwaitPlayDamage(damage);
        }

        public IEnumerator AwaitDeath()
        {
            //var messageCommand = new DialogCommand(attackConfig.DeathMessage);
            //yield return messageCommand.Await();
            
            yield return _damageAndDeathEffect.AwaitPlayDeath();
            
            //var killMessageCommand = new MonologueCommand(_killLocalizedString);
            //yield return killMessageCommand.Await();

            var kills = Lua.Run("return Variable[\"KILLS\"]").AsInt;
            kills += 1;
            Lua.Run($"Variable[\"KILLS\"] = {kills}");

            if (kills >= 4)
            {
                GameData.CharacterController.HatPoint.FreakShow(true);
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.HypnosisSound);
            }
            
            GameData.SaveLoadManager.Save();
            EventBus.Kill?.Invoke();
            yield return null;
        }
    }
}