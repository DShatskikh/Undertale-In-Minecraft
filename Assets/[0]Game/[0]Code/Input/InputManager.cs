using RimuruDev;
using UnityEngine;
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