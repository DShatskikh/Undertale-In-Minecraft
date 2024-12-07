using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EnemyAttackCommand : CommandBase
    {
        private readonly AttackBase _attackPrefab;
        private readonly BattleArena _arena;

        public EnemyAttackCommand(AttackBase attackPrefab)
        {
            _attackPrefab = attackPrefab;
            _arena = GameData.Battle.SessionData.Arena;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitAttack(action));
        }

        private IEnumerator AwaitAttack(UnityAction action)
        {
            GameData.HeartController.gameObject.SetActive(true);
            GameData.HeartController.enabled = true;
            _arena.gameObject.SetActive(true);
            AttackBase attack = GameData.Battle.CreateAttack(_attackPrefab);
            var isEndAttack = false;
            attack.Execute(() => isEndAttack = true);
            yield return new WaitUntil(() => isEndAttack);
            Object.Destroy(attack.gameObject);
            
            GameData.HeartController.enabled = false;
            action?.Invoke();
        }
    }
}