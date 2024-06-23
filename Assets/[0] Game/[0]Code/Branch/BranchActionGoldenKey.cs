using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Game
{
    public class BranchActionGoldenKey : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsGoldKey;
        }
    }
}