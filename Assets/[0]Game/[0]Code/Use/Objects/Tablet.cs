using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Game
{
    public class Tablet : UseObject
    {
        [Header("Data")]
        [SerializeField]
        private LocalizedString _text;
        
        [Header("Links")]
        [SerializeField]
        private GameObject _hud;

        [SerializeField]
        private TMP_Text _label;
        
        [SerializeField]
        private Button _button;

        private bool _isLoad;

        private void OnEnable()
        {
            _button.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Close);
        }

        public override void Use()
        {
            GameData.CharacterController.enabled = false;
            
            if (!_isLoad)
                StartCoroutine(AwaitLoadText());
            
            _hud.SetActive(true);
        }

        private void Close()
        {
            _hud.SetActive(false);
            GameData.CharacterController.enabled = true;
        }
        
        private IEnumerator AwaitLoadText()
        {
            var loadTextCommand = new LoadTextCommand(_text);
            yield return loadTextCommand.Await();
            _label.text = loadTextCommand.Result;
            _isLoad = true;
        }
    }
}