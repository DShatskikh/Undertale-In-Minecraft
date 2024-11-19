using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SlimeAttack_1 : AttackBase
    {
        [SerializeField]
        private SlimeToCharacterShell _slimeShell;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int i = 0; i < 6; i++)
            {
                var attack = Instantiate(_slimeShell, transform);
                attack.transform.position =
                    GameData.Battle.Arena.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 2f;

                //attack.SetTarget(GameData.HeartController.transform);
                yield return new WaitForSeconds(1);
            }
            
            yield return new WaitForSeconds(3);
            action?.Invoke();
        }
    }
}