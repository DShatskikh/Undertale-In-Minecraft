using UnityEngine.Events;

namespace Game
{
    public class CheckEndBattleCommand : CommandBase
    {
        public override void Execute(UnityAction action)
        {
            if (GameData.Battle.SessionData.Progress >= 100)
            {
                GameData.CommandManager.StopExecute();
                //GameData.Battle.EndBattle();
            }

            action.Invoke();
        }
    }
}