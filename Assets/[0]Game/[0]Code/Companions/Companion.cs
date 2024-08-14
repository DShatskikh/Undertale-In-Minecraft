using UnityEngine;

namespace Game
{
    public class Companion : MonoBehaviour
    {
        [SerializeField]
        private float _maxDistance;

        [SerializeField]
        private float _speed;
        
        [SerializeField]
        private float _distanceFromTarget;
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Vector2 _currentPoint;
        private float _currentSpeed;
        private Vector2 _previousPosition;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _currentPoint = GameData.Character.transform.position;
            transform.position = _currentPoint;
        }

        public void Update()
        {
            var characterPosition = GameData.Character.transform.position;
            var distance = Vector2.Distance(transform.position, characterPosition);

            transform.position = Vector2.MoveTowards(transform.position, 
                _currentPoint, _speed * Time.deltaTime);
            
            if (distance > _maxDistance + _distanceFromTarget)
            {
                _currentPoint = (Vector2)characterPosition + ((Vector2)(transform.position - characterPosition)).normalized * _distanceFromTarget;
                _spriteRenderer.flipX = characterPosition.x - transform.position.x < 0;
            }
            
            _animator.SetFloat("Speed", _currentSpeed > 0 ? 1 : 0);
        }

        private void FixedUpdate()
        {
            _currentSpeed = ((Vector2)((Vector3)_previousPosition - transform.position)).magnitude;
            _previousPosition = transform.position;
        }
    }
}