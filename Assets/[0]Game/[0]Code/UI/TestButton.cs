using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class TestButton : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        private Button _button;
        
        public void Init(string text, UnityAction action)
        {
            _button = GetComponent<Button>();
            _label.text = text;
            _button.onClick.AddListener(action);
        }
    }
}