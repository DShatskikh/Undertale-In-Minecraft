using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GideManager : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        
        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private GideButton _prefab;
        
        [SerializeField]
        private List<GideConfig> _configs;
        
        private List<GideButton> _buttons = new List<GideButton>();
        
        private void Start()
        {
            foreach (var config in _configs)
            {
                var button = Instantiate(_prefab, _container);
                button.Init(this, config);
                _buttons.Add(button);
            }

            Select(_buttons[0]);
        }

        public void Select(GideButton gide)
        {
            foreach (var button in _buttons) 
                button.UnSelect();

            _icon.sprite = gide.GetData.Picture;
            _label.text = gide.GetData.Info.GetLocalizedString();
            gide.Select();
        }
    }
}