using TMPro;
using UnityEngine;

namespace Game
{
    public class RebindKey : MonoBehaviour
    {
        [SerializeField]
        private SettingKeyScreen _settingKeyScreen;

        [SerializeField]
        private TMP_Text _label;

        //_label.text = ((KeySlotViewModel)_settingKeyScreen.CurrentSlot).;
        //_settingKeyScreen.Select();
        public void SetText(string text)
        {
            _label.text = text;
        }

        public void UpdateBindingDisplay()
        {
            
        }
    }
}