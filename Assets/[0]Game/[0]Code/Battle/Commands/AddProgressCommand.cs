using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class AddProgressCommand : CommandBase
    {
        private readonly int _progress;
        private readonly PopUpLabel _popUpLabel;
        private readonly AddProgressData _data;

        public AddProgressCommand(int progress, PopUpLabel popUpPopUpLabel, AddProgressData data)
        {
            _progress = progress;
            _popUpLabel = popUpPopUpLabel;
            _data = data;
        }
        
        public override void Execute(UnityAction action)
        {
            var progress = GameData.Battle.SessionData.Progress + _progress;
            
            if (progress > 100)
                progress = 100;
            
            if (progress < 0)
                progress = 0;

            GameData.Battle.SessionData.Progress = progress;
            GameData.Startup.StartCoroutine(AwaitAnimation(action));
        }

        private IEnumerator AwaitAnimation(UnityAction action)
        {
            var startMessage = _progress > 0 ? $"+{_progress}" : $"{_progress}";
            var color = _progress > 0 ? _data.MoreColor : _data.LessColor;

            LocalizedString message = _progress switch
            {
                > 3 => _data.MoreEpic,
                > 0 => _data.More,
                > -7 => _data.Less,
                _ => _data.LessEpic
            };

            var messageOperation = message.GetLocalizedStringAsync();
            
            while (!messageOperation.IsDone)
                yield return null;

            var result = messageOperation.Result;
            var sound = _progress > 0 ? _data.MoreSound : _data.LessSound;
            
            yield return _popUpLabel.AwaitAnimation(((MonoBehaviour)GameData.Battle.SelectEnemy).transform.position.AddY(1), startMessage, color, result, sound);
  
            EventBus.BattleProgressChange?.Invoke(GameData.Battle.SessionData.Progress);
            action?.Invoke();
        }
    }
}