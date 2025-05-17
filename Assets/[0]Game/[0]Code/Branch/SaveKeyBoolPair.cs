using System;

namespace Game
{
    [Serializable]
    public struct SaveKeyBoolPair
    {
        public SaveKeyBool Key;
        public bool Value;

        public SaveKeyBoolPair(SaveKeyBool key, bool value)
        {
            Key = key;
            Value = value;
        }
    }
}