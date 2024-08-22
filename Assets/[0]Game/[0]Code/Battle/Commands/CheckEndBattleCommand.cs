using UnityEngine.Events;

namespace Game
{
    public class CheckEndBattleCommand : CommandBase
    {
        public override void Execute(UnityAction action)
        {
            if (GameData.BattleProgress >= 100 && GameData.CommandManager.CurrentCommand is not StartEnemyTurnCommand)
            {
                GameData.CommandManager.StopExecute();
                GameData.Battle.EndBattle();
            }

            action.Invoke();
        }
    }
}