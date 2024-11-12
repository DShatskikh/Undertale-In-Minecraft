using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class AddProgressCommand : CommandBase
    {
        private readonly int _progress;
        private readonly TMP_Text _label;
        private readonly AddProgressData _data;

        public AddProgressCommand(int progress, TMP_Text label, AddProgressData data)
        {
            _progress = progress;
            _label = label;
            _data = data;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.BattleProgress += _progress;
            
            if (GameData.BattleProgress > 100)
                GameData.BattleProgress = 100;
            
            if (GameData.BattleProgress < 0)
                GameData.BattleProgress = 0;
            
            if (_progress > 0)
            {
                _label.color = _data.MoreColor;
                _label.text = $"+{_progress}";
            }
            else
            {
                _label.color = _data.LessColor;
                _label.text = $"{_progress}";
            }
            
            GameData.Startup.StartCoroutine(AwaitAnimation(action));
        }

        private IEnumerator AwaitAnimation(UnityAction action)
        {
            var duration = 0.25f;
            
            _label.gameObject.SetActive(true);
            var rectTransform = _label.GetComponent<RectTransform>();
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            var progress = 0.0f;

            var startScale = new Vector2(2, 0.2f);
            _label.transform.localScale = startScale;
            var startPosition = GameData.EnemyData.Enemy.transform.position.AddY(1);
            _label.transform.position = startPosition;

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

            while (progress < 1)
            {
                progress += Time.deltaTime / duration;
                _label.transform.localScale = Vector2.Lerp(startScale, Vector2.one, progress);
                _label.transform.position = Vector2.Lerp(startPosition, startPosition.AddX(1), progress);
                yield return null;
            }
            
            yield return new WaitForSeconds(0.25f);

            _label.text = result;
            GameData.EffectSoundPlayer.Play(_progress > 0 ? _data.MoreSound : _data.LessSound);

            duration = 1f;
            rectTransform.pivot = new Vector2(0.5f, 0);
            progress = 0;
            startPosition = _label.transform.position;

            EventBus.BattleProgressChange?.Invoke(GameData.BattleProgress);
            
            while (progress < 1)
            {
                progress += Time.deltaTime / duration;
                _label.color = _label.color.SetA(1 - progress);
                _label.transform.localScale = Vector2.Lerp(Vector2.one, new Vector2(1, 2), progress);
                _label.transform.position = Vector2.Lerp(startPosition, startPosition.AddY(1), progress);
                yield return null;
            }
            
            _label.gameObject.SetActive(false);
            action?.Invoke();
        }
    }
}