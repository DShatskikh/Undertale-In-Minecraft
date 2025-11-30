using System.Collections;
using UnityEngine;

namespace Game.Sleep
{
    public sealed class SleepBackground : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] _sprites;

        private IEnumerator Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();

            while (true)
            {
                spriteRenderer.sprite = _sprites[0];
                yield return new WaitForSeconds(1);
                spriteRenderer.sprite = _sprites[1];
                yield return new WaitForSeconds(1);
            }
        }
    }
}