using UnityEngine.Events;

namespace Game
{
    public abstract class CommandBase
    {
        public abstract void Execute(UnityAction action);
    }
}