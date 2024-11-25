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
            var index = Random.Range(0, _points.Length);
            Create(_points[index], index);
            yield return new WaitForSeconds(7f);
            action.Invoke();
        }
        
        private DragonShell Create(Transform point, int index)
        {
            var sell = Instantiate(_dragonShell, point.position, Quaternion.identity, transform);
            sell.SetDirection(index == 0 ? 1 : -1);
            sell.transform.localScale = point.localScale;
            return sell;
        }
    }
}