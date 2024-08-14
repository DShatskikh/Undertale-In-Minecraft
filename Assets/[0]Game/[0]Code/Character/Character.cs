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
        private UseArea _useArea;

        [SerializeField]
        private HatPoint _hatPoint;

        [SerializeField]
        private CharacterStep _characterStep;

        public CharacterView View => _view;
        public UseArea UseArea => _useArea;
        public HatPoint HatPoint => _hatPoint;

        private void Update()
        {
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            var isRun = Input.GetButton("Cancel");

            if (direction == Vector2.zero && GameData.Joystick.Direction.magnitude > 0.5f)
            {
                direction = GameData.Joystick.Direction.normalized;
                isRun = GameData.Joystick.Direction.magnitude >= 0.9f;
            }
            
            _mover.Move(direction, isRun);
            
            if (direction.magnitude > 0)
            {
                if (direction.x > 0) 
                    _view.Flip(false);
                
                if (direction.x < 0) 
                    _view.Flip(true);

                _view.Step();
                _characterStep.Execute(isRun);
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
            _useArea.enabled = false;
        }

        private void OnEnable()
        {
            _useArea.enabled = true; 
        }

        public void StopMove()
        {
            _mover.Move(Vector2.zero, false);
        }
    }
}