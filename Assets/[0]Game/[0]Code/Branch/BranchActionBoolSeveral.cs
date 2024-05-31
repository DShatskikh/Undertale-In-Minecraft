using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BranchActionBoolSeveral : BranchActionBase
    {
        [SerializeField]
        private List<SaveKeyBoolPair> _values = new();
        
        public override bool IsTrue()
        {
            if (_values.Count == 0)
                throw new Exception("Нету ключа в " + gameObject.name);
            
            foreach (var value in _values)
            {
                if (value.Value != GameData.Saver.LoadKey(value.Key))
                    return false;
            }

            return true;
        }
    }
}