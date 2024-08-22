using UnityEngine;
using UnityEngine.Rendering;

namespace Game
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField] 
        private Animator _animator;

        private SortingGroup _sortingGroup;
        private CharacterModel _model;

        public void SetModel(CharacterModel model)
        {
            _model = model;
            _model.SpeedChange += OnSpeedChange;
            _model.DirectionChange += OnDirectionChange;

            _sortingGroup = _spriteRenderer.GetComponent<SortingGroup>();
        }

        private void OnDestroy()
        {
            _model.SpeedChange -= OnSpeedChange;
            _model.DirectionChange -= OnDirectionChange;
        }

        private void OnSpeedChange(float speed)
        {
            _animator.SetBool("IsMove", speed > 0);
        }

        private void OnDirectionChange(Vector2 value)
        {
            if (value.x > 0) 
                Flip(false);
                
            if (value.x < 0) 
                Flip(true);
        }

        public void Flip(bool isFlip)
        {
            _spriteRenderer.flipX = isFlip;
        }

        public void Damage()
        {
            _animator.SetTrigger("Damage");
        }

        public void SetOrderInLayer(int value)
        {
            _sortingGroup.sortingOrder = value;
        }
    }
}