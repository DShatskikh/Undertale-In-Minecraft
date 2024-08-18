using UnityEngine.Events;

namespace Game
{
    public class StartCharacterTurnCommand : CommandBase
    {
        public override void Execute(UnityAction action)
        {
            GameData.HeartController.gameObject.SetActive(false);
            GameData.Battle.StartCharacterTurn();
            action.Invoke();
        }
    }
}