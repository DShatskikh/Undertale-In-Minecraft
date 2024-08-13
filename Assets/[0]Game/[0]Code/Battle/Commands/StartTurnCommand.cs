using UnityEngine.Events;

namespace Game
{
    public class StartTurnCommand : CommandBase
    {
        public override void Execute(UnityAction action)
        {
            action.Invoke();
            GameData.Battle.Turn();
        }
    }
}