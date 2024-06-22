using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

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
        
        private void Start()
        {
            _slider.onValueChanged.AddListener(ChangeValue);
            
            _slider.value = GameData.Volume;
            ChangeValue(GameData.Volume);
            UpdateText();
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(ChangeValue);
        }

        private void ChangeValue(float value)
        {
            GameData.Volume = value;
            GameData.Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, value));
            UpdateText();
        }

        private void UpdateText()
        {
            _localizedString.Arguments = new List<object>() { (int)(_slider.value * 100) };
            _text.text = _localizedString.GetLocalizedString(); //$"Звук {}%";
        }
    }
}