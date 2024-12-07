using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class SlimeBattleController : MonoBehaviour, IBattleController
    {
        [SerializeField]
        private string _index;
        
        [SerializeField]
        private SlimeEnemy _slimeEnemy;

        [SerializeField]
        private int _damage;
        
        [SerializeField]
        private BattleArena _arenaPrefab;

        [SerializeField]
        private LocalizedString _progressName;

        private int _numberTurn;
        private int _indexAttack;
        
        public IEnemy[] GetEnemies() => 
            new IEnemy[] { _slimeEnemy };

        private void Start()
        {
            _slimeEnemy.Init(this);
        }

        public void StartBattle()
        {
            GameData.Battle.Init().StartBattle(this);
        }

        public void Turn()
        {
            if (_numberTurn == 0)
            {
                GameData.Battle.PlayerTurn();
            }
            else
            {
                var commands = new List<CommandBase>()
                {
                    new ShowArenaCommand(),
                    new EnemyAttackCommand(_slimeEnemy.GetConfig.Attacks[_indexAttack]),
                    new HideArenaCommand()
                };
            
                GameData.CommandManager.StartCommands(commands);
            }

            _numberTurn++;
        }

        public IEnumerator AwaitEndIntro()
        {
            GameData.CharacterController.View.Dance();
            yield return _slimeEnemy.AwaitEndIntro();
            GameData.CharacterController.View.Reset();

            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator AwaitActReaction(string actName, float value)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator AwaitDeadReaction(string enemyName)
        {
            throw new System.NotImplementedException();
        }

        public BattleArena GetArena() => 
            _arenaPrefab;

        public LocalizedString GetProgressName() => 
            _progressName;

        public string GetIndex => 
            _index;
        
        public int GetDamage() => 
            _damage;
    }
}