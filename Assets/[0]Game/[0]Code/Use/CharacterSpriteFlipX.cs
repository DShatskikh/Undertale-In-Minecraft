using UnityEngine;

namespace Game
{
    public class CharacterSpriteFlipX : MonoBehaviour
    {
        public void Use()
        {
            GameData.Character.View.Flip(true);
        }
    }
}