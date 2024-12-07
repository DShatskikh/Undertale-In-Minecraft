using System.Collections;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class BattleProgressBar : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;
        
        [SerializeField]
        private MMProgressBar _progressBar;

        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private MMF_Player _show;
        
        [SerializeField]
        private MMF_Player _hide;
        
        private string _labelName;
        
        private void OnEnable()
        {
            EventBus.BattleProgressChange += ChangeProgress;
            StartCoroutine(AwaitLoadLabel(GameData.Battle.SessionData.BattleController.GetProgressName()));
            
            _container.position = _hide.transform.position;
        }

        private void OnDisable()
        {
            EventBus.BattleProgressChange -= ChangeProgress;
        }

        public void Show()
        {
            _container.position = _hide.transform.position;
            _show.PlayFeedbacks();
        }

        public void Hide()
        {
            _container.position = _show.transform.position;
            _hide.PlayFeedbacks();
        }
        
        private void ChangeProgress(int value)
        {
            _progressBar.UpdateBar(value, 0, 100);
            _label.text = _labelName + $" {value}%";

            var addProgress = GameData.Battle.SessionData.AddProgress;
            
            if (addProgress == 0)
                _label.text += $" (+?)";
            else if (addProgress > 0)
                _label.text += $"<Color=green> (+{addProgress}) </Color>";
            else if (addProgress < 0)
                _label.text += $"<Color=red> ({addProgress}) </Color>";
        }
        
        private IEnumerator AwaitLoadLabel(LocalizedString localizedString)
        {
            var loadTextCommand = new LoadTextCommand(localizedString);
            yield return loadTextCommand.Await().ContinueWith(() => _labelName = loadTextCommand.Result);
            
            _progressBar.SetBar(0, 0, 100);
            ChangeProgress(GameData.Battle.SessionData.Progress);
        }
    }
}