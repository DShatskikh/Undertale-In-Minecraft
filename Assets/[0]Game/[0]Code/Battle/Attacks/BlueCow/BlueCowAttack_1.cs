using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class BlueCowAttack_1 : AttackBase
    {
        [SerializeField]
        private StarShell _starShell;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int j = 0; j < 6; j++)
            {
                var shells = new List<StarShell>();

                for (int i = 0; i < 3; i++)
                {
                    shells.Add(Create());
                    yield return new WaitForSeconds(0.5f);
                }

                foreach (var shell in shells)
                {
                    shell.StartMove();
                }

                yield return new WaitForSeconds(2);
            }
        }
        
        private StarShell Create()
        {
            return Instantiate(_starShell, new Vector3(Random.Range(-2.7f, 2.7f), 
                Random.Range(0, 2) == 1 ? -1.5f : 1.7f), Quaternion.identity, transform);
        }
    }
}