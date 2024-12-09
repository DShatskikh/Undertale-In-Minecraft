using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ExitMenuScreen : MonoBehaviour
    {
        [SerializeField]
        private MMF_Player _selectPlayer;
        
        [SerializeField]
        private GameplayMenu _gameplayMenu;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private ExitMenuButton _buttonPrefab;
        
        private List<MenuButton> _buttons = new();

        private void OnEnable()
        {
            foreach (var config in GameData.AssetProvider.ExitSlotConfigs)
            {
                var button = Instantiate(_buttonPrefab, _container);
                button.Init(config);
                _buttons.Add(button);
            }

            for (var index = 0; index < _buttons.Count; index++)
            {
                var button = _buttons[index];
                var navigation = button.navigation;
                navigation.mode = Navigation.Mode.Vertical;
                
                if (index != 0) 
                    navigation.selectOnUp = _buttons[index - 1];

                if (index != _buttons.Count - 1) 
                    navigation.selectOnDown = _buttons[index + 1];
                
                button.navigation = navigation;
            }
        }

        private void OnDisable()
        {
            foreach (var button in _buttons) 
                Destroy(button.gameObject);

            _buttons = new List<MenuButton>();
        }
    }
}