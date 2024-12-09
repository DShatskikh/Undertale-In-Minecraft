using UnityEngine;
using YG;

namespace Game
{
    public class AddGoldKey : MonoBehaviour
    {
        public void Use()
        {
            YandexGame.savesData.IsGoldKey = true;
        }
    }
}