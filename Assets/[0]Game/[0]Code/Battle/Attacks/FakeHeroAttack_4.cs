using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class FakeHeroAttack_4 : AttackBase
    {
        [SerializeField]
        private SwordRotationShell _swordRotationShell;

        [SerializeField]
        private LightningShell _lightningShell;

        [SerializeField]
        private GameObject _swordShellGroup_1, _swordShellGroup_2;

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int i = 0; i < 10; i++)
            {
                var index = Random.Range(0, 4);

                switch (index)
                {
                    case 1: 
                        var shell = Instantiate(_lightningShell, transform);
                        shell.transform.position = GameData.HeartController.transform.position;
                        break;
                    case 2: 
                        Instantiate(_swordRotationShell, transform).transform.position =
                            transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 5;
                        break;
                    case 3: 
                        Instantiate(_swordShellGroup_1, transform).transform.position =
                            GameData.HeartController.transform.position;
                        break;
                    case 4: 
                        Instantiate(_swordShellGroup_2, transform).transform.position =
                            GameData.HeartController.transform.position;
                        break;
                    default:
                        break;
                }

                yield return new WaitForSeconds(1);
            }

            action?.Invoke();
        }
    }
}