using System;
using System.Collections;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class DamageEvent : Saver
    {
        [SerializeField]
        private EnemyBase _enemy;
        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private GameObject _fire;

        [SerializeField]
        private int _startHealth;

        [SerializeField]
        private MMProgressBar _progressBar;

        [SerializeField] 
        private TMP_Text _label;
        
        [SerializeField]
        private GameObject _explosion;

        [SerializeField]
        private MMF_Player _shake;

        [SerializeField]
        private LocalizedString _killLocalizedString;
        
        private int _health;
        private Data _saveData = new();
        
        public int GetHealth => _health;
        
        [Serializable]
        public class Data
        {
            public bool IsDead;
        }

        public override void OnEnable() { }
        public override void OnDisable() { }

        public override void Start()
        {
            base.Start();
            SaveSystem.RegisterSaver(this);
        }

        public override void OnDestroy()
        {
            SaveSystem.UnregisterSaver(this);
        }
        
        public override string RecordData()
        {
            return SaveSystem.Serialize(_saveData);
        }

        public override void ApplyData(string s)
        {
            var data = SaveSystem.Deserialize(s, _saveData);
            _saveData = data;

            if (_saveData.IsDead)
            {
                _health = 0; 
                _enemy.gameObject.SetActive(false);
            }
            else
            {
                _health = _startHealth;
                _label.text = $"{_health}/{_startHealth}";
                _progressBar.SetBar(_health, 0, _startHealth);
            }
        }

        public IEnumerator AwaitEvent(EnemyBase enemy, int damage = 0)
        {
            print($"Damage: {damage}");
            
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.DamageSound);
            GameData.CharacterController.View.ShowLine(transform.position.AddY(0.5f));
            yield return new WaitForSeconds(0.25f);
            GameData.CharacterController.View.HideLine();
            _fire.SetActive(true);

            for (int i = 0; i < 3; i++)
            {
                _spriteRenderer.color = new Color(1, 163 / 255f, 163 / 255f);
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.BombSound);
                yield return new WaitForSeconds(0.25f);
                _spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(0.25f);
            }

            _fire.SetActive(false);

            //_health -= _health; //
            
            _health -= damage;

            if (_health < 0)
                _health = 0;
            
            var damageCommand = new DamageEnemyCommand(_progressBar, _startHealth, _health, damage, _label);
            yield return damageCommand.Await();

            AttackActConfig attackConfig = null;

            foreach (var act in enemy.GetConfig.Acts)
            {
                if (act is AttackActConfig attackActConfig)
                {
                    attackConfig = attackActConfig;
                    break;
                }
            }

            if (_health > 0)
            {
                //yield return new WaitForSeconds(1f);
                
                var messageCommand = new MessageCommand(GameData.Battle.EnemyMessageBox, attackConfig.Reaction);
                yield return messageCommand.Await();
                var addProgressCommand = new AddProgressCommand(-GameData.BattleProgress, GameData.Battle.AddProgressLabel,
                    GameData.Battle.AddProgressData);
                addProgressCommand.Execute(null);
                yield return new WaitForSeconds(1f);
                var startTurn = new StartEnemyTurnCommand();
                startTurn.Execute(null);
            }
            else
            {
                GameData.CommandManager.StopExecute();
                GameData.Battle.EndBattle();
            }
        }

        public IEnumerator AwaitDeathEvent(EnemyBase enemy, float value = 0)
        {
            //yield return new WaitForSeconds(1);
            
            AttackActConfig attackConfig = null;

            foreach (var act in enemy.GetConfig.Acts)
            {
                if (act is AttackActConfig attackActConfig)
                {
                    attackConfig = attackActConfig;
                    break;
                }
            }
            
            _shake.PlayFeedbacks();

            var messageCommand = new DialogCommand(attackConfig.DeathMessage);
            yield return messageCommand.Await();

            _shake.StopFeedbacks();

            yield return new WaitForSeconds(0.5f);
            
            _explosion.SetActive(true);
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.BombSound);
            yield return new WaitForSeconds(0.3f);
            enemy.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            _explosion.SetActive(false);
            gameObject.SetActive(false);

            var killMessageCommand = new MonologueCommand(_killLocalizedString);
            yield return killMessageCommand.Await();
            
            var kills = Lua.Run("return Variable[\"KILLS\"]").AsInt;
            kills += 1;
            Lua.Run($"Variable[\"KILLS\"] = {kills}");

            if (kills >= 4)
            {
                GameData.CharacterController.HatPoint.FreakShow(true);
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.HypnosisSound);
            }

            _saveData.IsDead = true;
            GameData.SaveLoadManager.Save();
            EventBus.Kill?.Invoke();
            yield return null;
        }
    }
}