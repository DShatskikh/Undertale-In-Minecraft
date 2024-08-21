using UnityEngine;

namespace Game
{
    public class OffArena : MonoBehaviour
    {
        public void Use()
        {
            GameData.Battle.Arena.SetActive(false);
        }
    }
}