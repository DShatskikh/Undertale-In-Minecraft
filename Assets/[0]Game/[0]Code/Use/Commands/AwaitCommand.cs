using System.Collections;
using UnityEngine.Events;

namespace Game
{
    public abstract class AwaitCommand : CommandBase
    {
        protected bool _isAction = false;
        protected UnityAction _action;

        public AwaitCommand()
        {
            _action = () => _isAction = true;
        }
        
        public abstract IEnumerator Await();
    }
}