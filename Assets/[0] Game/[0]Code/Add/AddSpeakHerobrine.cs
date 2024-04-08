using UnityEngine;

namespace Game
{
    public class AddSpeakHerobrine : AddBase
    {
        public override void Use()
        {
            GameData.IsSpeakHerobrine = true;
        }
    }
}