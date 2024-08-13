using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Attack : AttackBase
    {
        public float Delay = 10f;

        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitExecute(action));
        }

        private IEnumerator AwaitExecute(UnityAction action)
        {
            yield return new WaitForSeconds(Delay);
            action.Invoke();
        }
    }
}