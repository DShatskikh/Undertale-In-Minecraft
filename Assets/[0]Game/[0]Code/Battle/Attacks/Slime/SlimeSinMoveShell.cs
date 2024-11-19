using System.Collections;
using UnityEngine;

namespace Game
{
    public class SlimeSinMoveShell : SlimeShellBase
    {
        private int _amplitude;

        public void SetTarget(Vector3 direction)
        {
            StartCoroutine(AwaitMove(direction));
        }

        private IEnumerator AwaitMove(Vector3 direction)
        {
            while (true)
            {
                yield return AwaitMoveToPoint(transform.position + direction.AddY(Random.Range(0, 2) == 0 ? -1 : 1));
            }
        }
    }
}