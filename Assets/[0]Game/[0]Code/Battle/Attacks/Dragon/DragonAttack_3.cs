using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class DragonAttack_3 : AttackBase
    {
        [SerializeField]
        private FireballShell _fireballShell;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(_fireballShell, 
                    transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity, transform);
                yield return new WaitForSeconds(0.4f);
            }
            
            yield return new WaitForSeconds(0.4f);
            action.Invoke();
        }
    }
}