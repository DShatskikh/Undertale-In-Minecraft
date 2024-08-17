using UnityEngine;

namespace Game
{
    public class ChangeColor : MonoBehaviour
    {
        [SerializeField]
        private Color _color;

        public void Use()
        {
            GetComponent<SpriteRenderer>().color = _color;
        }
    }
}