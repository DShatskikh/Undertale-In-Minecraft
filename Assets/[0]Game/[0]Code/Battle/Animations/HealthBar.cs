using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;

namespace Game
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private MMProgressBar _progressBar;
        
        [SerializeField] 
        private TMP_Text _label;

        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private MMF_Player _show;
        
        [SerializeField]
        private MMF_Player _hide;
        
        private void OnEnable()
        {
            var maxHealth = Lua.Run("Variable[\"MaxHealth\"]").AsInt;
            EventBus.HealthChange += UpdateHealthView;
            _progressBar.SetBar(maxHealth, 0, maxHealth);
            UpdateHealthView(maxHealth,GameData.HeartController.Health);
            
            _container.position = _hide.transform.position;
        }

        private void OnDisable()
        {
            EventBus.HealthChange -= UpdateHealthView;
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
        
        private void UpdateHealthView(int maxValue, int value)
        {
            _label.text = $"{value}/{maxValue}";
            _progressBar.UpdateBar(value, 0, maxValue);
        }
    }
}