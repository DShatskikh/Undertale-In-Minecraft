using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{
    public class BlueCowAttack_1 : AttackBase
    {
        [FormerlySerializedAs("_starShell")]
        [SerializeField]
        private StarToCharacterShell starToCharacterShell;
        
        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            for (int j = 0; j < 8; j++)
            {
                var shells = new List<StarToCharacterShell>();

                for (int i = 0; i < 5; i++)
                {
                    shells.Add(Create());
                    yield return new WaitForSeconds(0.25f);
                }

                foreach (var shell in shells)
                {
                    shell.StartMove();
                }

                yield return new WaitForSeconds(1.5f);
            }
            
            action.Invoke();
        }
        
        private StarToCharacterShell Create()
        {
            var shell =Instantiate(starToCharacterShell, new Vector3(Random.Range(-2.7f, 2.7f), 
                Random.Range(0, 2) == 1 ? -1.5f : 1.7f), Quaternion.identity, transform);
            shell.transform.localScale = Vector3.one * 0.75f;
            return shell;
        }
    }
}