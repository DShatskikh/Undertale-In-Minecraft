using UnityEngine;

namespace Game
{
    public class ChangeSprite : MonoBehaviour
    {
        [SerializeField]
        private Sprite _sprite;

        public void Change()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _sprite;
        }
    }
}