using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class DialogCommand : AwaitCommand
    {
        private readonly Replica[] _replicas;
        private readonly UnityEvent _endEvent;
        private readonly AudioClip _sound;

        public DialogCommand(Replica[] replicas, UnityEvent endEvent = null, AudioClip sound = null)
        {
            _replicas = replicas;
            _endEvent = endEvent;
            _sound = sound;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Dialog.Show(_replicas, _sound);
            EventBus.CloseDialog += () => OnCloseDialog(action);
        }
        
        private void OnCloseDialog(UnityAction action)
        {
            _endEvent?.Invoke();
            action.Invoke();
        }

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            throw new System.NotImplementedException();
        }
    }
}