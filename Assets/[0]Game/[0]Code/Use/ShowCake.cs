using UnityEngine;

namespace Game
{
    public class ShowCake : MonoBehaviour
    {
        public void Use()
        {
            GameData.Character.HatPoint.Show();
        }
    }
}