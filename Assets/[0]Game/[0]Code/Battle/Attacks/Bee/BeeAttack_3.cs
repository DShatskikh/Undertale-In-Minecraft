using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Game
{
    public class BeeAttack_3 : AttackBase
    {
        [SerializeField]
        private BeePuddleSell _beePuddleSell;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int i = 0; i < 10; i++)
            {
                Create();
                yield return new WaitForSeconds(1);
            }
            
            action?.Invoke();
        }

        private void Create()
        {
            Instantiate(_beePuddleSell, new Vector3(Random.Range(-1.7f, 1.7f), Random.Range(-2f, -0.582f)), Quaternion.identity, transform);
        }
    }
}