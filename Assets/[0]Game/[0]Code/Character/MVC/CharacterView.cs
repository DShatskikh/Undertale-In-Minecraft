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

        [SerializeField]
        private LineRenderer _lineRenderer;
        
        private SortingGroup _sortingGroup;
        private CharacterModel _model;

        private static readonly int DamageHash = Animator.StringToHash("Damage");
        private static readonly int MoveHash = Animator.StringToHash("IsMove");
        private static readonly int StartFlyHash = Animator.StringToHash("StartFly");
        private static readonly int EndFlyHash = Animator.StringToHash("EndFly");
        private static readonly int StateHash = Animator.StringToHash("State");

        private enum CharacterState
        {
            Idle = 0,
            IdleDance = 1,
            Damage = 2,
            Move = 3,
            Fly = 4,
            Sleep = 5,
            Sit = 6
        }
        
        public void SetModel(CharacterModel model)
        {
            _model = model;
            _model.SpeedChange += OnSpeedChange;
            _model.DirectionChange += OnDirectionChange;
            _model.FlyChange += OnFlyChange;

            _sortingGroup = _spriteRenderer.GetComponent<SortingGroup>();
        }

        private void OnDestroy()
        {
            _model.SpeedChange -= OnSpeedChange;
            _model.DirectionChange -= OnDirectionChange;
            _model.FlyChange -= OnFlyChange;
        }

        private void OnSpeedChange(float speed)
        {
            _animator.SetBool(MoveHash, speed > 0);
            //_animator.SetFloat(StateHash, (float)(speed > 0 ? CharacterState.Move : CharacterState.Idle));
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
            _animator.SetTrigger(DamageHash);
        }

        public void SetOrderInLayer(int value)
        {
            _sortingGroup.sortingOrder = value;
        }

        private void OnFlyChange(bool value)
        {
            //_animator.SetTrigger(value ? StartFlyHash : EndFlyHash);
            _animator.SetFloat(StateHash, (float)(value ? CharacterState.Fly : CharacterState.Idle));
        }

        public void Dance()
        {
            _animator.SetFloat(StateHash, (float)(CharacterState.IdleDance));
        }

        public void Sleep()
        {
            _animator.SetFloat(StateHash, (float)(CharacterState.Sleep)); 
        }

        public void Sit()
        {
            _animator.SetFloat(StateHash, (float)(CharacterState.Sit)); 
        }
        
        public void Reset()
        {
            _animator.SetFloat(StateHash, (float)(CharacterState.Idle)); 
        }

        public void ShowLine(Vector2 target)
        {
            var positions = new[] { Vector3.zero, (Vector3)target - _lineRenderer.transform.position };
            _lineRenderer.SetPositions(positions);
            _lineRenderer.gameObject.SetActive(true);
        }

        public void HideLine()
        {
            _lineRenderer.gameObject.SetActive(false);
        }

        public Sprite GetSprite() => 
            _spriteRenderer.sprite;
    }
}