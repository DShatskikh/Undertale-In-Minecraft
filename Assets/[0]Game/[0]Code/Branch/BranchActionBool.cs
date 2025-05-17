using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class BranchActionBool : BranchActionBase
    {
        [SerializeField]
        private SaveKeyBoolPair value;
        
        public override bool IsTrue()
        {
            if (value.Key == null)
                throw new Exception("Нету ключа в " + gameObject.name);
            
            return value.Value == GameData.Saver.LoadKey(value.Key);
        }
    }
}