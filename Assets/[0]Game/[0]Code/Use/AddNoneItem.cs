using UnityEngine;
using YG;

namespace Game
{
    public class AddNoneItem : MonoBehaviour
    {
        public void Add()
        {
            YandexGame.savesData.IsNoneItem = true;
            YandexGame.savesData.IsNoneItemFirst = true;
        }
    }
}