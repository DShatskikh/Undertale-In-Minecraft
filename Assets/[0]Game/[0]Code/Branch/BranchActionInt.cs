using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class BranchActionInt : BranchActionBase
    {
        [SerializeField]
        private SaveKeyIntPair _value;
        
        public override bool IsTrue()
        {
            return GameData.Saver.LoadKey(_value.Key) == _value.Value;
        }
    }
}