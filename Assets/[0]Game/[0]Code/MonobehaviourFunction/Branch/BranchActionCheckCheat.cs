using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using YG;

namespace Game
{
    public class BranchActionCheckCheat : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsCheat;
        }
    }
}