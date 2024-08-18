using UnityEngine.Events;

namespace Game
{
    public class StartEnemyTurnCommand : CommandBase
    {
        public override void Execute(UnityAction action)
        {
            GameData.Battle.Turn();
        }
    }
}