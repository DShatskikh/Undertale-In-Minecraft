using System;
using PixelCrushers;
using UnityEngine;

namespace Game
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] 
        private CharacterView _view;

        [SerializeField]
        private UseArea _useArea;

        [SerializeField]
        private HatPoint _hatPoint;

        [SerializeField]
        private CharacterStep _characterStep;
        
        private Rigidbody2D _rigidbody;
        private CharacterModel _model;
        private CharacterMover _mover;
        private Vector3 _previousPosition;

        public CharacterView View => _view;
        public UseArea UseArea => _useArea;
        public HatPoint HatPoint => _hatPoint;
        public CharacterModel Model => _model;

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _model = new CharacterModel();
            _view.SetModel(_model);
            _characterStep.SetModel(_model);
            _mover = new CharacterMover(_model, _rigidbody);
        }

        private void Update()
        {
            var playerInput = GameData.PlayerInput;
            _model.SetDirection(playerInput.actions["Move"].ReadValue<Vector2>().normalized);
            _model.IsRun = playerInput.actions["Cancel"].IsPressed();
            _mover.Move(_model.Direction,  _model.IsRun);
        }
        
        private void FixedUpdate()
        {
            _model.SetSpeed(((Vector2)(_previousPosition - transform.position)).magnitude);
            _previousPosition = transform.position;
        }

        public void OnEnable()
        {
            _useArea.enabled = true; 
        }

        public void OnDisable()
        {
            _mover.Move(Vector2.zero, false);
            _model.SetSpeed(0);
            _useArea.enabled = false;

            EventBus.Cancel = null;
            EventBus.CancelUp = null;
            EventBus.Submit = null;
            EventBus.SubmitUp = null;
        }
    }
}