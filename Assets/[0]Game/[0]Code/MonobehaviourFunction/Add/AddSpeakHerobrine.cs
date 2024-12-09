using UnityEngine;
using YG;

namespace Game
{
    public class AddSpeakHerobrine : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsSpeakHerobrine = true;
        }
    }
}