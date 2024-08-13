using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class MessageCommand : CommandBase
    {
        private readonly BattleMessageBox _messageBox;
        private readonly BattleMessageData[] _messages;

        private int _index;
        
        public MessageCommand(BattleMessageBox messageBox, BattleMessageData[] messages)
        {
            _messageBox = messageBox;
            _messages = messages;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitMessageShow(action));
        }

        private IEnumerator AwaitMessageShow(UnityAction action)
        {
            while (_index < _messages.Length)
            {
                var message = _messages[_index];
                var messageOperation = message.LocalizedString.GetLocalizedStringAsync();
            
                while (!messageOperation.IsDone)
                    yield return null;

                var result = messageOperation.Result;
                
                _messageBox.Show(result, message.Shaking);
                bool isSubmit = false; 
                EventBus.Submit = () => isSubmit = true;
                yield return new WaitUntil(() => isSubmit);
                _index++;
            }
            
            _messageBox.Hide();
            action.Invoke();
        }
    }
}