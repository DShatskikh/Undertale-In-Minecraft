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
            yield return _bigSlime.AwaitMoveToPoint();
        }
    }
}