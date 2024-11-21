using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class DragonAttack_2 : AttackBase
    {
        [SerializeField]
        private DragonShell _dragonShell;

        [SerializeField]
        private Transform[] _points;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            Create(_points[Random.Range(0, _points.Length)]);
            yield return new WaitForSeconds(7f);
            action.Invoke();
        }
        
        private DragonShell Create(Transform point)
        {
            var sell = Instantiate(_dragonShell, point.position, Quaternion.identity, transform);
            sell.transform.localScale = point.localScale;
            return sell;
        }
    }
}