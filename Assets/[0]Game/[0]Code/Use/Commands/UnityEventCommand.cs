using UnityEngine.Events;

namespace Game
{
    public class UnityEventCommand : CommandBase
    {
        private UnityEvent _event;
        
        public UnityEventCommand(UnityEvent @event)
        {
            _event = @event;
        }

        public override void Execute(UnityAction action)
        {
            _event.Invoke();
            action.Invoke();
        }
    }
}