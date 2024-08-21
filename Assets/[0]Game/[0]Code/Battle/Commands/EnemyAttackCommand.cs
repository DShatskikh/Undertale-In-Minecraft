using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EnemyAttackCommand : CommandBase
    {
        private readonly AttackBase _attackPrefab;
        private readonly BlackPanel _blackPanel;
        private readonly GameObject _arena;

        public EnemyAttackCommand(AttackBase attackPrefab, BlackPanel blackPanel, GameObject arena)
        {
            _attackPrefab = attackPrefab;
            _blackPanel = blackPanel;
            _arena = arena;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitAttack(action));
        }

        private IEnumerator AwaitAttack(UnityAction action)
        {
            GameData.HeartController.gameObject.SetActive(true);
            GameData.HeartController.enabled = true;
            _arena.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            AttackBase attack = GameData.Battle.CreateAttack(_attackPrefab);
            var isEndAttack = false;
            attack.Execute(() => isEndAttack = true);
            yield return new WaitUntil(() => isEndAttack);
            Object.Destroy(attack.gameObject);

            _blackPanel.Hide();
            GameData.HeartController.enabled = false;
            action.Invoke();
        }
    }
}