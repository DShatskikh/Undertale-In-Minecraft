using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class BranchActionBase : MonoBehaviour
    {
        [SerializeField] 
        private UnityEvent _trueEvent, _falseEvent;

        public abstract bool IsTrue();
        
        public void Use()
        {
            if (IsTrue())
                _trueEvent.Invoke();
            else
                _falseEvent.Invoke();
        }
    }
}