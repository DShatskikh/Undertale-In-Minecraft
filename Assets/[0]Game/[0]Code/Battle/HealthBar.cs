using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using YG;

namespace Game
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private MMProgressBar _progressBar;
        
        [SerializeField] 
        private TMP_Text _label;

        private void OnEnable()
        {
            EventBus.HealthChange += UpdateHealthView;
            _progressBar.SetBar(YandexGame.savesData.MaxHealth, 0, YandexGame.savesData.MaxHealth);
            UpdateHealthView(YandexGame.savesData.MaxHealth,YandexGame.savesData.Health);
        }

        private void OnDisable()
        {
            EventBus.HealthChange -= UpdateHealthView;
        }

        private void UpdateHealthView(int maxValue, int value)
        {
            _label.text = $"{value}/{maxValue}";
            _progressBar.UpdateBar(value, 0, maxValue);
        }
    }
}