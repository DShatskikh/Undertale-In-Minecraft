using System;
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

        [SerializeField]
        private Item _item;
        
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
            if (GameData.Battle.SessionData.Progress >= 100)
            {
                //GameData.Battle.EndBattle();
                return;
            }
            
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
                    new HideArenaCommand(),
                    new StartCharacterTurnCommand()
                };

                GameData.CommandManager.StartCommands(commands);
                _indexAttack++;

                if (_indexAttack > _slimeEnemy.GetConfig.Attacks.Length)
                    _indexAttack = 0;
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
            switch (actName)
            {
                case "Attack":
                    yield return _slimeEnemy.AwaitDamage((int)value * 100);

                    if (_slimeEnemy.Health <= 0)
                    {
                        yield return AwaitDeadReaction(_slimeEnemy.GetConfig.name);
                        yield break;
                    }
                    
                    break;
                default:
                    throw new Exception();
            }
            
            Turn();
        }

        public IEnumerator AwaitDeadReaction(string enemyName)
        {
            yield return _slimeEnemy.AwaitDeath();
            yield return GameData.Battle.AwaitEndBattle();
            //_slimeEnemy.gameObject.SetActive(false);
            _item.gameObject.SetActive(true);
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