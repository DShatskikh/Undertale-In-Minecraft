using System;
using RimuruDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private Button _submitButton;
        
        [SerializeField]
        private Button _menuButton;
        
        [SerializeField]
        private Joystick _joystick;

        public Vector2 MoveInput;

        private PlayerInput _playerInput;

        private InputAction _moveAction;

        private void Awake()
        {
            //_playerInput = GetComponent<PlayerInput>();

            //SetupInputActions();
        }

        private void Update()
        {
            //UpdateInputs();
        }

        private void SetupInputActions()
        {
            _moveAction = _playerInput.actions["Move"];
            
        }

        private void UpdateInputs()
        {
            //MoveInput = _moveAction.ReadValue<Vector2>();
        }

        public void Show()
        {
            _submitButton.gameObject.SetActive(true);

            if (GameData.DeviceType == CurrentDeviceType.Mobile)
            {
                _joystick.gameObject.SetActive(true);
                _menuButton.gameObject.SetActive(true);
            }
        }

        public void Hide()
        {
            _submitButton.gameObject.SetActive(false);
            _menuButton.gameObject.SetActive(false);
            _joystick.gameObject.SetActive(false); 
        }
    }
}