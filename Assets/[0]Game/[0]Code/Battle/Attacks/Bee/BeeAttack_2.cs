using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class BeeAttack_2 : AttackBase
    {
        [SerializeField]
        private BeeShell _shell;

        [SerializeField]
        private Transform _spawnPoint;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(1);
                Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(-1);
                yield return new WaitForSeconds(0.5f);
            }

            action?.Invoke();
        }
    }
}