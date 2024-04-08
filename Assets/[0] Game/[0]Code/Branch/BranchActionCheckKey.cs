using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{
    public class BranchActionCheckKey : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsPrisonKey;
        }
    }
}