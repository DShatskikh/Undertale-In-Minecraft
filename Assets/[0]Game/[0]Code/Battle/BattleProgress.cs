using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BattleProgress : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;
        
        [SerializeField]
        private Slider _slider;

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
            _label.text = $"Прогресс битвы {value}%";
        }
    }
}