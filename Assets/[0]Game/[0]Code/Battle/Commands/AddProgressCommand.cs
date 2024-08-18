using UnityEngine.Events;

namespace Game
{
    public class AddProgressCommand : CommandBase
    {
        private readonly int _progress;

        public AddProgressCommand(int progress)
        {
            _progress = progress;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.BattleProgress += _progress;
            
            if (GameData.BattleProgress > 100)
                GameData.BattleProgress = 100;
            
            EventBus.BattleProgressChange?.Invoke(GameData.BattleProgress);
            action.Invoke();
        }
    }
}