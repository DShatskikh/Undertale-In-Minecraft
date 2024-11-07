using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class BranchActionLua : BranchActionBase
    {
        [SerializeField]
        private string _conditions;
        
        public override bool IsTrue()
        {
            return Lua.IsTrue(_conditions);
        }
    }
}