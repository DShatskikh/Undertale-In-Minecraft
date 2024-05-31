using UnityEngine;

namespace Game
{
    public class ColorsOff : MonoBehaviour
    {
        [ContextMenu("Сделать обьекты прозрачными")]
        private void AlphaOff()
        {
            foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.color = spriteRenderer.color.SetA(0);
            }       
        }
        
        [ContextMenu("Сделать обьекты не прозрачными")]
        private void AlphaOn()
        {
            foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.color = spriteRenderer.color.SetA(1);
            } 
        }
    }
}