using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class MonologueCommand : AwaitCommand
    {
        private readonly LocalizedString[] _replicas;
        private readonly UnityEvent _endEvent;
        
        public MonologueCommand(LocalizedString[] replicas, UnityEvent endEvent)
        {
            _replicas = replicas;
            _endEvent = endEvent;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Monolog.Show(_replicas);
            EventBus.CloseMonolog += () => OnCloseMonolog(action);
        }

        public override IEnumerator Await()
        {
            Execute(_action);
            yield return new WaitUntil(() => _isAction);
        }

        private void OnCloseMonolog(UnityAction action)
        {
            _endEvent?.Invoke();
            action.Invoke();
        }
    }
}