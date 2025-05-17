using UnityEngine;

namespace Game
{
    public class ShowHackerMask : MonoBehaviour
    {
        public void Use()
        {
            GameData.Character.HackerMask.Show();
        }
    }
}