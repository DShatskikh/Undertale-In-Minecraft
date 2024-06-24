using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private LocalizedString _localizedString;

        public float GetCurrentVolume => _slider.value;
        
        private void Start()
        {
            _slider.onValueChanged.AddListener(ChangeValue);
         
            _localizedString.Arguments = new List<object>() { (int)(_slider.value * 100) };
            _localizedString.StringChanged += UpdateText;

            _slider.value = YandexGame.savesData.Volume;
            ChangeValue(YandexGame.savesData.Volume);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(ChangeValue);
            _localizedString.StringChanged -= UpdateText;
        }

        private void ChangeValue(float value)
        {
            YandexGame.savesData.Volume = value;
            GameData.Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, value));
            _localizedString.Arguments = new List<object>() { (int)(_slider.value * 100) };
            _localizedString.RefreshString();
        }

        private void UpdateText(string text)
        {
            _text.text = text; //$"Звук {}%";
        }
    }
}