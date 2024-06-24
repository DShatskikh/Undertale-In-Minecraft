using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Game
{
    public class BattleProgress : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;
        
        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private LocalizedString _localizedString;
        
        private void OnEnable()
        {
            EventBus.OnBattleProgressChange += ChangeProgress;
        }

        private void OnDisable()
        {
            EventBus.OnBattleProgressChange -= ChangeProgress;
        }

        private void Start()
        {
            ChangeProgress(GameData.BattleProgress);
        }

        private void ChangeProgress(int value)
        {
            _slider.value = value;

            _localizedString.Arguments = new object[] { value };
            _label.text = _localizedString.GetLocalizedString(); //$"Прогресс битвы {value}%";
        }
    }
}