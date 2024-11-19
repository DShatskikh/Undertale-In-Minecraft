using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SlimeAttack_3 : AttackBase
    {
        [SerializeField]
        private SlimeSinMoveShell _slimeSinMoveShell;

        [SerializeField]
        private Transform _spawnPoint;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int i = 0; i < 10; i++)
            {
                var attack = Instantiate(_slimeSinMoveShell, transform);
                attack.SetTarget(Vector2.left);
                attack.transform.position = _spawnPoint.position.AddY(Random.Range(-2f, 2f));

                //attack.SetTarget(GameData.HeartController.transform);
                yield return new WaitForSeconds(1);
            }
            
            yield return new WaitForSeconds(2);
            action?.Invoke();
        }
    }
}