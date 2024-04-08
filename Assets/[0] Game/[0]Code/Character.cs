using System;
using UnityEngine;

namespace Game
{
    public class Character : MonoBehaviour
    {
        [SerializeField] 
        private CharacterMover _mover;

        [SerializeField] 
        private CharacterView _view;

        [SerializeField] 
        private AudioSource _stepSource1, _stepSource2;

        [SerializeField] 
        private float _intervalStep;

        [SerializeField]
        private UseArea _useArea;

        [SerializeField]
        private HatPoint _hatPoint;
        
        private float _currentStepTime;
        private bool _isStepRight;
        public CharacterView View => _view;
        public UseArea UseArea => _useArea;
        public HatPoint HatPoint => _hatPoint;

        private void Start()
        {
            _currentStepTime = _intervalStep;
        }

        private void Update()
        {
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if (direction == Vector2.zero && GameData.Joystick.Direction.magnitude > 0.5f)
                direction = GameData.Joystick.Direction.normalized;
            
            _mover.Move(direction);
            
            if (direction.magnitude > 0)
            {
                if (direction.x > 0) 
                    _view.Flip(false);
                
                if (direction.x < 0) 
                    _view.Flip(true);

                _view.Step();
                _currentStepTime += Time.deltaTime;
                
                if (_currentStepTime >= _intervalStep)
                {
                    _currentStepTime = 0;

                    if (_isStepRight)
                        _stepSource1.Play();
                    else
                        _stepSource2.Play();

                    _isStepRight = !_isStepRight;
                }
            }
            else
            {
                _view.Idle();
            }
        }

        private void OnDisable()
        {
            StopMove();
            _view.Idle();
        }

        public void StopMove()
        {
            _mover.Move(Vector2.zero);
        }
    }
}