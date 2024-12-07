using UnityEngine;

namespace Game
{
    public class OffArena : MonoBehaviour
    {
        public void Use()
        {
            GameData.Battle.SessionData.Arena.gameObject.SetActive(false);
        }
    }
}