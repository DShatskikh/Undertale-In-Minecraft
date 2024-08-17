using UnityEngine;
using YG;

namespace Game
{
    public class AddPrisonKey : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsPrisonKey = true;
        }
    }
}