using UnityEngine;

namespace Game
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField] 
        private Animator _animator;
        
        public void Flip(bool isFlip)
        {
            _spriteRenderer.flipX = isFlip;
        }

        public void Damage()
        {
            _animator.SetTrigger("Damage");
        }

        public void Step()
        {
            _animator.SetBool("IsMove", true);
        }

        public void Idle()
        {
            _animator.SetBool("IsMove", false);
        }
    }
}