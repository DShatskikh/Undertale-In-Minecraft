using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{
    public class BlueCowAttack_2 : AttackBase
    {
        [SerializeField]
        private StarShell starSell;

        [SerializeField]
        private Transform[] _points;

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int j = 0; j < 8; j++)
            {
                foreach (var point in _points)
                {
                    var shell = Create(point.position);
                    shell.StartMove();
                    yield return new WaitForSeconds(0.75f);
                }
            }
            
            yield return new WaitForSeconds(2f);
            action.Invoke();
        }

        private StarShell Create(Vector3 point)
        {
            var sell = Instantiate(starSell, point, Quaternion.identity, transform);
            sell.transform.localScale = Vector3.one * 1.5f;
            sell.Direction = new Vector3( point.x - transform.position.x > 1 ? -1 : 1, 0);
            return sell;
        }
    }
}