using System.Collections;
using UnityEngine;

namespace Game
{
    public class SwordShell : Shell
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        private IEnumerator Start()
        {
            
            
            var moveToBackCommand = new MoveToPointCommand(transform, transform.position.AddX(-1), 2);
            yield return moveToBackCommand.Await();
        }
    }
}