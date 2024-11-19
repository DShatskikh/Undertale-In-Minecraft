using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SlimeAttack_2 : AttackBase
    {
        [SerializeField]
        private Transform _upTarget, _middleTarget, _downTarget;

        [SerializeField]
        private Transform _upStartPoint, _middleStartPoint, _downStartPoint;

        [SerializeField]
        private SlimeShell _slimeShell;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            Create(_upStartPoint, _upTarget);
            Create(_middleStartPoint, _middleTarget);
            Create(_downStartPoint, _downTarget);
            
            yield return new WaitForSeconds(3f);

            var previousIndex = 4;
            
            for (int i = 0; i < 10; i++)
            {
                var index = Random.Range(0, 3);

                if (previousIndex == index)
                    index = (index + 1) % 3;
                
                previousIndex = index;
                
                switch (index)
                {
                    case 0:
                        Create(_upStartPoint, _upTarget);
                        break;
                    case 1:
                        Create(_middleStartPoint, _middleTarget);
                        break;
                    case 2:
                        Create(_downStartPoint, _downTarget);
                        break;
                    default:
                        break;
                }
                
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(1.5f);
            action?.Invoke();
        }

        private SlimeShell Create(Transform createPoint, Transform endPoint)
        {
            var shell = Instantiate(_slimeShell, createPoint.position, Quaternion.identity, transform);
            shell.SetTarget(endPoint);
            return shell;
        }
    }
}