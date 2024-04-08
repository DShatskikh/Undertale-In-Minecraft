using UnityEngine;

namespace Game
{
    public class AddGoldKey : MonoBehaviour
    {
        public void Use()
        {
            GameData.IsGoldKey = true;
        }
    }
}