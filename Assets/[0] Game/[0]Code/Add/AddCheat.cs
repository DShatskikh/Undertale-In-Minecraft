using UnityEngine;

namespace Game
{
    public class AddCheat : MonoBehaviour
    {
        public void Use()
        {
            GameData.IsCheat = true;
        }
    }
}