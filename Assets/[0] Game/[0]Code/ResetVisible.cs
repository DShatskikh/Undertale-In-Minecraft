using UnityEngine;

namespace Game
{
    public class ResetVisible : MonoBehaviour
    {
        public void Use()
        {
            GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color.SetA(1);
        }
    }
}