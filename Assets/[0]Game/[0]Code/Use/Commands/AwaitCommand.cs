using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class AwaitCommand : CommandBase
    {
        protected bool _isAction;
        protected UnityAction _action;

        protected AwaitCommand()
        {
            _action = () => _isAction = true;
        }
        
        public virtual IEnumerator Await()
        {
            Execute(_action);
            yield return new WaitUntil(() => _isAction);
        }

        protected abstract IEnumerator AwaitExecute(UnityAction action);
    }
}