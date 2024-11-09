using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Attack : AttackBase
    {
        public float Delay = 10f;

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            yield return new WaitForSeconds(Delay);
            action.Invoke();
        }
    }
}