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
        
        public MessageCommand(BattleMessageBox messageBox, BattleMessageData message)
        {
            _messageBox = messageBox;
            _messages = new []{ message };
        }
        
        public override void Execute(UnityAction action)
        {
            _messageBox.Show(_messages, action);
        }
    }
}