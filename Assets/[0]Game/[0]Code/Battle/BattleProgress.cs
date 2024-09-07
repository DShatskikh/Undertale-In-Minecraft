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

        [SerializeField]
        private LocalizedString _localizedString;
        
        private void OnEnable()
        {
            EventBus.BattleProgressChange += ChangeProgress;
        }

        private void OnDisable()
        {
            EventBus.BattleProgressChange -= ChangeProgress;
        }

        private void Start()
        {
            _progressBar.SetBar(100, 0, 100);
            ChangeProgress(GameData.BattleProgress);
        }

        private void ChangeProgress(int value)
        {
            _progressBar.UpdateBar(value, 0, 100);

            _localizedString.Arguments = new object[] { value };
            _label.text = _localizedString.GetLocalizedString();
        }
    }
}