using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EnemyAttackCommand : CommandBase
    {
        private AttackBase _attackPrefab;
        private BlackPanel _blackPanel;
        
        public EnemyAttackCommand(AttackBase attackPrefab, BlackPanel blackPanel)
        {
            _attackPrefab = attackPrefab;
            _blackPanel = blackPanel;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitAttack(action));
        }

        private IEnumerator AwaitAttack(UnityAction action)
        {
            GameData.HeartController.gameObject.SetActive(true);
            GameData.Arena.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            AttackBase attack = GameData.Battle.CreateAttack(_attackPrefab);
            var isEndAttack = false;
            attack.Execute(() => isEndAttack = true);
            yield return new WaitUntil(() => isEndAttack);
            Object.Destroy(attack.gameObject);
                
            _blackPanel.Hide();
            action.Invoke();
        }
    }
}