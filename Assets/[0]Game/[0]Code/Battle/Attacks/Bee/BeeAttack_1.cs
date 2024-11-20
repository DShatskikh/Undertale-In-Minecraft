using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class BeeAttack_1 : AttackBase
    {
        [SerializeField]
        private BeeShell _shell;

        [SerializeField]
        private Transform _spawnPoint;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(1);
            yield return new WaitForSeconds(2f);
            
            Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(-1);
            yield return new WaitForSeconds(3f);
            
            Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(1);
            Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(-1);
            yield return new WaitForSeconds(2f);
            
            Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(1);
            Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(-1);
            yield return new WaitForSeconds(1f);
            
            Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(1);
            Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(-1);
            yield return new WaitForSeconds(1f);
            
            Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(1);
            Instantiate(_shell, _spawnPoint.position, Quaternion.identity, transform).Init(-1);
            yield return new WaitForSeconds(1f);
            
            action?.Invoke();
        }
    }
}