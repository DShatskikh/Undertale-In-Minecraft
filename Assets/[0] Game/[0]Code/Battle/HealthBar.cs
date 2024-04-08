using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            EventBus.OnHealthChange += UpdateHealthView;
            UpdateHealthView(GameData.MaxHealth,GameData.Health);
        }

        private void OnDisable()
        {
            EventBus.OnHealthChange -= UpdateHealthView;
        }

        private void UpdateHealthView(int maxValue, int value)
        {
            _label.text = $"{value}/{maxValue}";
            _slider.value = value;
            _slider.maxValue = maxValue;
        }
    }
}