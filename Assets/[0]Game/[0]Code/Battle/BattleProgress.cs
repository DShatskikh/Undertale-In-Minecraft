using System.Collections;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class BattleProgress : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;
        
        [SerializeField]
        private MMProgressBar _progressBar;

        private string _labelName;
        
        private void OnEnable()
        {
            EventBus.BattleProgressChange += ChangeProgress;
            StartCoroutine(AwaitLoadLabel(GameData.EnemyData.EnemyConfig.ProgressLocalized));
        }

        private void OnDisable()
        {
            EventBus.BattleProgressChange -= ChangeProgress;
        }

        private void ChangeProgress(int value)
        {
            _progressBar.UpdateBar(value, 0, 100);
            _label.text = _labelName + $" {value}%";

            var addProgress = GameData.Battle.AddProgress;
            
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
            
            _progressBar.SetBar(100, 0, 100);
            ChangeProgress(GameData.BattleProgress);
        }
    }
}