using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public struct SaveKeyIntPair
    {
        public SaveKeyInt Key;
        public int Value;

        public SaveKeyIntPair(SaveKeyInt key, int value)
        {
            Key = key;
            Value = value;
        }
    }
}