using UnityEngine;

namespace Game
{
    public class BranchActionPalesos : BranchActionBase
    {
        [SerializeField]
        private int _value;

        public override bool IsTrue()
        {
            return GameData.Palesos == _value;
        }
    }
}