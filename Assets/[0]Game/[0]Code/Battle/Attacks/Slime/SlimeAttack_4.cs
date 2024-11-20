using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SlimeAttack_4 : AttackBase
    {
        [SerializeField]
        protected SlimeShellBigJump _bigSlime;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            _bigSlime.StartMoveToPoint();
            yield return new WaitForSeconds(6);
            yield return new WaitForSeconds(4);
            action?.Invoke();
        }
    }
}