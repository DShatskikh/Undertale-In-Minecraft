using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class FakeHeroAttack_1 : AttackBase
    {
        [SerializeField]
        private GameObject _swordShellGroup_1, _swordShellGroup_2;

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(_swordShellGroup_1, transform).transform.position =
                    GameData.HeartController.transform.position;

                yield return new WaitForSeconds(1);
                
                Instantiate(_swordShellGroup_2, transform).transform.position =
                    GameData.HeartController.transform.position;
                
                yield return new WaitForSeconds(1);
            }
            
            action?.Invoke();
        }
    }
}