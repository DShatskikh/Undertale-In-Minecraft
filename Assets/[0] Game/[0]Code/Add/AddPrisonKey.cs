using UnityEngine;

namespace Game
{
    public class AddPrisonKey : AddBase
    {
        public override void Use()
        {
            GameData.IsPrisonKey = true;
        }
    }
}