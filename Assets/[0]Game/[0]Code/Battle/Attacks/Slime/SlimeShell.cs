using System.Collections;
using UnityEngine;

namespace Game
{
    public class SlimeShell : SlimeShellBase
    {
        private Transform _target;
        
        public void SetTarget(Transform target)
        {
            _target = target;

            StartCoroutine(AwaitMove());
        }

        private IEnumerator AwaitMove()
        {
            var startDirection = (_target.position - transform.position).normalized;
            bool isFinish = false;

            while (true)
            {
                if (Vector3.Distance(_target.position, transform.position) < 0.5f)
                    isFinish = true;

                yield return AwaitMoveToPoint(isFinish ? transform.position + startDirection * 2 : _target.position);
            }
        }
    }
}