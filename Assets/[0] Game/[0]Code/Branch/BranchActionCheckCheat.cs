using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{
    public class BranchActionCheckCheat : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsCheat;
        }
    }
}