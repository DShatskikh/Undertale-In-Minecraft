using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class JoystickView : MonoBehaviour
    {
        [SerializeField]
        private Image[] _images;
    
        [SerializeField]
        private Color _offColor, _onColor;
    
        private void Update()
        {
            AllOff();
            
            var image = GetImage();
            
            if (image && GameData.Joystick.Direction.magnitude > 0.5f)
                image.color = _onColor;
        }
    
        private void AllOff()
        {
            foreach (var image in _images) 
                image.color = _offColor;
        }

        private Image GetImage()
        {
            var x = GameData.Joystick.Direction.x;
            var y =  GameData.Joystick.Direction.y;
        
            if (x is > -0.5f and < 0.5f && y > 0.5f)
                return _images[0];

            if (y is > -0.5f and < 0.5f && x > 0.5f)
                return _images[2];
            
            if (x is > -0.5f and < 0.5f && y < -0.5f)
                return _images[4];
            
            if (y is > -0.5f and < 0.5f && x < -0.5f)
                return _images[6];
            
            if (x > 0f && y > 0f)
                return _images[1];
            
            if (x > 0f && y < 0f)
                return _images[3];
            
            if (x < 0f && y < 0f)
                return _images[5];
                        
            if (x < 0f && y > 0f)
                return _images[7];

            return null;
        }
    }
}