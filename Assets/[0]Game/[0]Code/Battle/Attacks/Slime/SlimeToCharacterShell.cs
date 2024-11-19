using System.Collections;
using UnityEngine;

namespace Game
{
    public class SlimeToCharacterShell : SlimeShellBase
    {
        private IEnumerator Start()
        {
            while (true)
            {
                yield return AwaitMoveToPoint(GameData.HeartController.transform.position);
            }
        }
    }
}