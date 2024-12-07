using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game
{
    public class DanceActScreen : UIPanelBase
    {
        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private Image _target;

        [SerializeField]
        private Image _arrowPrefab;

        [SerializeField]
        private Transform _startPoint;

        [SerializeField]
        private Transform _targetPoint;

        private Image _currentArrow;
        private Coroutine _coroutine;
        private DanceActConfig _config;
        private int _currentArrowIndex;
        private bool _isSuccess;
        private Arrow[] _arrows;

        public void Init(DanceActConfig config)
        {
            _config = config;
            Activate(true);
            
            _arrows = new Arrow[4];

            for (int i = 0; i < _arrows.Length; i++) 
                _arrows[i] = (Arrow)Random.Range(0, 4);

            StartCoroutine(AwaitIntro());
        }

        private IEnumerator AwaitIntro()
        {
            _isSuccess = true;
            
            var moveToPointCommand = new MoveToPointCommand(GameData.CharacterController.transform, 
                GameData.CharacterController.transform.position.SetX(GameData.Battle.transform.position.x), 1);
            yield return moveToPointCommand.Await();
            
            _coroutine = StartCoroutine(AwaitActive());
        }
        
        private IEnumerator AwaitActive()
        {
            var startDistanceX = _startPoint.position.x - _target.transform.position.x;
            
            _currentArrow = Instantiate(_arrowPrefab, new Vector3(_target.transform.position.x + startDistanceX, _target.transform.position.y), quaternion.identity, _container);
            _currentArrow.gameObject.SetActive(true);
            _currentArrow.color = _currentArrow.color.SetA(0);

            _currentArrow.transform.eulerAngles = _currentArrow.transform.rotation.eulerAngles.SetZ(
                _arrows[_currentArrowIndex] switch
                {
                    Arrow.Up => 0,
                    Arrow.Down => 180,
                    Arrow.Right => 270,
                    Arrow.Left => 90,
                    _ => 0
                });
            
            var changeAlphaCommand = new ChangeAlphaImageCommand(_currentArrow, 1, 0.5f);
            yield return changeAlphaCommand.Await();

            var progress = 0f;
            var startPositionX = _currentArrow.transform.position.x;

            while (progress < 1)
            {
                progress += Time.deltaTime / 1;
                yield return null;
                
                _currentArrow.transform.position = _currentArrow.transform.position.SetX(Mathf.Lerp(startPositionX, _target.transform.position.x - startDistanceX, progress));

                _target.color = Vector3.Distance(_target.transform.position, _currentArrow.transform.position) 
                                < Vector3.Distance(_target.transform.position, _targetPoint.position)
                    ? GameData.AssetProvider.SelectColor 
                    : Color.white;

                _currentArrow.color = _target.color;
            }

            StartCoroutine(AwaitEndMove(Arrow.Up));
        }

        private IEnumerator AwaitEndMove(Arrow currentArrow)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            GameData.CharacterController.View.Dance();
            
            if (Vector3.Distance(_target.transform.position, _currentArrow.transform.position)
                < Vector3.Distance(_target.transform.position, _targetPoint.position) && currentArrow == _arrows[_currentArrowIndex])
            {
                _currentArrow.transform.position = _target.transform.position;
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SelectSound);
                _target.color = Color.white;
                _currentArrow.color = Color.white;
            }
            else
            {
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.HurtSound);
                _target.color = Color.red;
                _currentArrow.color = Color.red;
                _isSuccess = false;
            }

            var changeAlpha0Command = new ChangeAlphaImageCommand(_currentArrow, 0, 0.5f);
            _currentArrow = null;
            yield return changeAlpha0Command.Await();
            
            GameData.CharacterController.View.Reset();

            _currentArrowIndex += 1;
            
            if (_currentArrowIndex < _arrows.Length)
                _coroutine = StartCoroutine(AwaitActive());
            else
                StartCoroutine(AwaitEnding());
        }

        private IEnumerator AwaitEnding()
        {
            var moveToPointCommand = new MoveToPointCommand(GameData.CharacterController.transform, 
                GameData.Battle.SessionData.SquadOverWorldPositionsData[0].Point.position, 1);
            yield return moveToPointCommand.Await();

            yield return GameData.Battle.SessionData.BattleController.AwaitActReaction(_config.name, _isSuccess ? 1 : 0);
            
            /*var commands = new List<CommandBase>();
            commands.Add(new MessageCommand(GameData.Battle.MessageBox, _isSuccess ? _config.SuccessSystemMessage : _config.FailedSystemMessage));
            commands.Add(new MessageCommand(GameData.Battle.EnemyMessageBox, _isSuccess ? _config.SuccessReaction : _config.FailedReaction));
            commands.Add(new AddProgressCommand(_isSuccess ? _config.SuccessProgress : _config.FailedProgress, GameData.Battle.AddProgressLabel, GameData.Battle.AddProgressData));
            GameData.CommandManager.StartCommands(commands);
            */
            
            Destroy(gameObject);
        }

        public override void OnSubmitDown()
        {

        }

        public override void OnSubmitUp()
        {
            
        }

        public override void OnCancel()
        {
            
        }

        public override void OnSlotIndexChanged(Vector2 direction)
        {
            var currentArrow = DirectionExtensions.GetArrow(direction);
            
            if (_currentArrow)
                StartCoroutine(AwaitEndMove(currentArrow));
        }
    }
}