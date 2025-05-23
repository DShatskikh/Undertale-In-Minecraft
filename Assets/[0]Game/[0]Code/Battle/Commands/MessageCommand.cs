using System.Collections;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class MessageCommand : AwaitCommand
    {
        private readonly BattleMessageBox _messageBox;
        private readonly LocalizedString[] _messages;

        private int _index;
        
        public MessageCommand(BattleMessageBox messageBox, LocalizedString[] messages)
        {
            _messageBox = messageBox;
            _messages = messages;
        }
        
        public MessageCommand(BattleMessageBox messageBox, LocalizedString message)
        {
            _messageBox = messageBox;
            _messages = new []{ message };
        }
        
        public override void Execute(UnityAction action)
        {
            _messageBox.Show(_messages, action);
        }

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            throw new System.NotImplementedException();
        }
    }
}