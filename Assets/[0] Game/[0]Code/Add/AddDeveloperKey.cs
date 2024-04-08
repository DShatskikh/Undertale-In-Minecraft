using UnityEngine;

namespace Game
{
    public class AddDeveloperKey : MonoBehaviour
    {
        public void Use()
        {
            GameData.IsDeveloperKey = true;
        }
    }
}