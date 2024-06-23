using UnityEngine;
using YG;

namespace Game
{
    public class AddDeveloperKey : MonoBehaviour
    {
        public void Use()
        {
            YandexGame.savesData.IsDeveloperKey = true;
        }
    }
}