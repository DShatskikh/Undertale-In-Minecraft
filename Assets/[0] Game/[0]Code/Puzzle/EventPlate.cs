using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EventPlate : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private TMP_Text _label;
        
        [SerializeField]
        private Color _pressedColor;
        
        [SerializeField]
        private Color _notPressedColor;
        
        [SerializeField] 
        private Sprite _pressedSprite;

        [SerializeField] 
        private Sprite _notPressedSprite;
        
        [SerializeField]
        private UnityEvent _event;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character character))
            {
                _label.color = _pressedColor;
                _spriteRenderer.sprite = _pressedSprite;
                _event.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character character))
            {
                _label.color = _notPressedColor;
                _spriteRenderer.sprite = _notPressedSprite;
            }
        }
    }
}