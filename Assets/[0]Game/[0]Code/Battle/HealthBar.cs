using MoreMountains.Tools;
using PixelCrushers.DialogueSystem;
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
            var maxHealth = Lua.Run("Variable[\"MaxHealth\"]").AsInt;
            EventBus.HealthChange += UpdateHealthView;
            _progressBar.SetBar(maxHealth, 0, maxHealth);
            UpdateHealthView(maxHealth,GameData.HeartController.Health);
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