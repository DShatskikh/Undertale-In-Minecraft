using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class DragonAttack_1 : AttackBase
    {
        [SerializeField]
        private DragonFireShell _dragonFireShell;

        [SerializeField]
        private Transform[] _points;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int i = 0; i < 14; i++)
            {
                Create(_points[Random.Range(0, _points.Length)]);
                yield return new WaitForSeconds(1.5f);
            }
            
            yield return new WaitForSeconds(2f);
            action.Invoke();
        }
        
        private DragonFireShell Create(Transform point)
        {
            var sell = Instantiate(_dragonFireShell, point.position, Quaternion.identity, transform);
            sell.transform.localScale = point.localScale;
            return sell;
        }
    }
}