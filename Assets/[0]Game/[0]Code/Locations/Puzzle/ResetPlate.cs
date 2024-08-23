using UnityEngine;

namespace Game
{
    public class ResetPlate : MonoBehaviour
    {
        [SerializeField]
        private PlatePuzzle _manager;
        
        [SerializeField] 
        private Sprite _activeSprite;

        [SerializeField] 
        private Sprite _deactivateSprite;

        private SpriteRenderer _spriteRenderer;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                if (!_spriteRenderer)
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                
                _manager.Deactivate();
                _spriteRenderer.sprite = _activeSprite;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                if (!_spriteRenderer)
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                
                _spriteRenderer.sprite = _deactivateSprite;
            }
        }
    }
}