using UnityEngine;

namespace Game
{
    public class OffArena : MonoBehaviour
    {
        public void Use()
        {
            GameData.Arena.SetActive(false);
        }
    }
}