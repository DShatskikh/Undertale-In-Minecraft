using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EnemyAttackCommand : CommandBase
    {
        private AttackBase _attackPrefab;
        private BlackPanel _blackPanel;
        private Coroutine _awaitAttackCoroutine;
        private AttackBase _attack;
        private UnityAction _action;

        public EnemyAttackCommand(AttackBase attackPrefab, BlackPanel blackPanel)
        {
            _attackPrefab = attackPrefab;
            _blackPanel = blackPanel;
        }
        
        public override void Execute(UnityAction action)
        {
            _awaitAttackCoroutine = GameData.Startup.StartCoroutine(AwaitAttack(action));
            GameData.Startup.StartCoroutine(AwaitCheckEnd());
        }

        private IEnumerator AwaitCheckEnd()
        {
            while (_awaitAttackCoroutine != null)
            {
                if (GameData.BattleProgress >= 100)
                {
                    GameData.Startup.StopCoroutine(_awaitAttackCoroutine);
                    EndAttack();
                    yield break;
                }

                yield return null;
            }
        }

        private IEnumerator AwaitAttack(UnityAction action)
        {
            _action = action;
            GameData.Arena.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _attack = GameData.Battle.CreateAttack(_attackPrefab);
            var isEndAttack = false;
            _attack.Execute(() => isEndAttack = true);
            yield return new WaitUntil(() => isEndAttack);
            EndAttack();
        }

        private void EndAttack()
        {
            Object.Destroy(_attack.gameObject);
                
            _blackPanel.Hide();

            GameData.BattleProgress += GameData.EnemyData.EnemyConfig.ProgressAttack;

            if (GameData.BattleProgress > 100)
                GameData.BattleProgress = 100;
                
            EventBus.BattleProgressChange?.Invoke(GameData.BattleProgress);
            _action.Invoke();
            _awaitAttackCoroutine = null;
        }
    }
}