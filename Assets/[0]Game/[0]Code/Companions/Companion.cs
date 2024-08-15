using UnityEngine;

namespace Game
{
    public class Companion : MonoBehaviour
    {
        [SerializeField]
        private float _maxDistance;

        [SerializeField]
        private float _speed, _speedRun;
        
        [SerializeField]
        private float _distanceFromTarget;
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Vector2 _currentPoint;
        private float _currentSpeed;
        private float _delayIdle;
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
            _previousPosition = transform.position;
        }

        public void Update()
        {
            var characterPosition = GameData.Character.transform.position;
            var distance = Vector2.Distance(transform.position, characterPosition);

            if (distance > _distanceFromTarget)
            {
                _currentPoint = (Vector2)characterPosition + ((Vector2)(transform.position - characterPosition)).normalized * _distanceFromTarget;
                _spriteRenderer.flipX = characterPosition.x - transform.position.x < 0;
            }

            /*if (_currentSpeed > 0)
                _delayIdle = 0.3f;
            else if (_delayIdle > 0)
                _delayIdle -= Time.deltaTime;*/
        }

        private void FixedUpdate()
        {
            _currentSpeed = ((Vector2)((Vector3)_previousPosition - transform.position)).magnitude;
            _previousPosition = transform.position;
            _animator.SetFloat("Speed", _currentSpeed > 0 ? 1 : 0);
            
            var characterPosition = GameData.Character.transform.position;
            var distance = Vector2.Distance(transform.position, characterPosition);
            var speed = distance > _maxDistance ? _speedRun : _speed;
            
            transform.position = Vector2.MoveTowards(transform.position, 
                _currentPoint, speed);
        }
    }
}