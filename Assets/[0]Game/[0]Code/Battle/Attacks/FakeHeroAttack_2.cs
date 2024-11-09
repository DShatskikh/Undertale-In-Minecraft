using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class FakeHeroAttack_2 : AttackBase
    {
        [SerializeField]
        private SwordRotationShell _swordRotationShell;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(_swordRotationShell, transform).transform.position =
                    transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 5;

                yield return new WaitForSeconds(1);
            }
            
            action?.Invoke();
        }
    }
}