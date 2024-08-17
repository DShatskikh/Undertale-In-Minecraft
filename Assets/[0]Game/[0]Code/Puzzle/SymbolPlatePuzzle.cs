using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public class SymbolPlatePuzzle : MonoBehaviour
    {
        [SerializeField]
        private char _symbol;

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
        private SymbolPlatePuzzleManager _puzzleManager;
        
        [SerializeField] 
        private PlaySoundEffect _playSound;
        
        private bool _isPressed;

        private void Start()
        {
            _label.text = _symbol.ToString();
        }

        public void PressView()
        {
            _isPressed = true;
            _label.color = _pressedColor;
            _spriteRenderer.sprite = _pressedSprite;
        }
        
        public void Press()
        {
            PressView();
            _playSound.Play();
            _puzzleManager.AddSymbol(_symbol);
        }

        public void ResetPlate()
        {
            _isPressed = false;
            _label.color = _notPressedColor;
            _spriteRenderer.sprite = _notPressedSprite;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                if (!_isPressed)
                {
                    Press();
                }
            }
        }
    }
}