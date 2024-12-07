using UnityEngine.Events;

namespace Game
{
    public class StartCharacterTurnCommand : CommandBase
    {
        public override void Execute(UnityAction action)
        {
            GameData.Battle.SessionData.ThemeTime = GameData.MusicPlayer.GetTime();
            GameData.MusicPlayer.Play(GameData.Battle.SessionData.SelectTheme);
            GameData.HeartController.gameObject.SetActive(false);
            GameData.Battle.PlayerTurn();
            action.Invoke();
        }
    }
}