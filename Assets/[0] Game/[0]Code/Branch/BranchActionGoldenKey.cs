using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class BranchActionGoldenKey : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsGoldKey;
        }
    }
}