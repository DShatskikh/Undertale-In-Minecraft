using System;
using UnityEngine;

namespace Game
{
    public class Plate : MonoBehaviour
    {
        [SerializeField] 
        private Sprite _activeSprite;

        [SerializeField] 
        private Sprite _deactivateSprite;
        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField] 
        private PlaySoundEffect _playSound;
        
        public bool IsActive;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character character) && !IsActive)
            {
                Activate();
                _playSound.Play();
            }
        }

        public void Activate()
        {
            _spriteRenderer.sprite = _activeSprite;
            IsActive = true;
            print(IsActive);
        }

        public void Deactivate()
        {
            print("Deactivate");
            _spriteRenderer.sprite = _deactivateSprite;
            IsActive = false;
        }
    }
}