using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] 
        private Slider _slider;
        
        [SerializeField] 
        private TMP_Text _label;

        private void OnEnable()
        {
            EventBus.HealthChange += UpdateHealthView;
            UpdateHealthView(YandexGame.savesData.MaxHealth,YandexGame.savesData.Health);
        }

        private void OnDisable()
        {
            EventBus.HealthChange -= UpdateHealthView;
        }

        private void UpdateHealthView(int maxValue, int value)
        {
            _label.text = $"{value}/{maxValue}";
            _slider.value = value;
            _slider.maxValue = maxValue;
        }
    }
}