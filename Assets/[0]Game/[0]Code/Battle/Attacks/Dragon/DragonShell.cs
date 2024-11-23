using UnityEngine;

namespace Game
{
    public class DragonShell : Shell
    {
        private void Update()
        {
            transform.position += transform.right * 1 * Time.deltaTime * transform.localScale.x * 2.5f;
        }
    }
}