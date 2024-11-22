using UnityEngine.Events;

namespace Game
{
    public class StartCharacterTurnCommand : CommandBase
    {
        public override void Execute(UnityAction action)
        {
            GameData.Battle.MusicStopTime = GameData.MusicPlayer.GetTime();
            GameData.MusicPlayer.Play(GameData.Battle.SelectMusic);
            GameData.HeartController.gameObject.SetActive(false);
            GameData.Battle.StartCharacterTurn();
            action.Invoke();
        }
    }
}