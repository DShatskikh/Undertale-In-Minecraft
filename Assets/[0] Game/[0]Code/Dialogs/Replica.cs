using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public struct Replica
    {
        public Sprite Icon;
        
        [TextArea]
        public string Text;
    }
}