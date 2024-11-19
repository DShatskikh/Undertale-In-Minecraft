using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class FakeHeroAttack_3 : AttackBase
    {
        [SerializeField]
        private LightningShell _lightningShell;

        [SerializeField]
        private Transform[] _points;

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int i = 0; i < 10; i++)
            {
                var shell = Instantiate(_lightningShell, transform);
                shell.transform.position = Random.Range(0, 2) == 0 ? _points[Random.Range(0, _points.Length)].position : GameData.HeartController.transform.position;

                yield return new WaitForSeconds(1);
            }

            action?.Invoke();
        }
    }
}