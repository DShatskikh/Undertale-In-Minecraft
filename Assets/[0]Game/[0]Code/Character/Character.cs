using System;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        [SerializeField]
        private HackerMask _hackerMask;
        
        private float _currentStepTime;
        private bool _isStepRight;
        public CharacterView View => _view;
        public UseArea UseArea => _useArea;
        public HatPoint HatPoint => _hatPoint;
        public HackerMask HackerMask => _hackerMask;

        private void Start()
        {
            _currentStepTime = _intervalStep;
        }

        private void Update()
        {
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

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

        private void OnEnable()
        {
            UseArea.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            StopMove();
            _view.Idle();
            UseArea.gameObject.SetActive(false);
        }

        public void StopMove()
        {
            _mover.Move(Vector2.zero);
        }
    }
}