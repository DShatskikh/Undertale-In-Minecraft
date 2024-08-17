using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class MoveToPointCommand : CommandBase
    {
        private Transform _transform;
        private Vector2 _target;
        private float _speed;
        
        public MoveToPointCommand(Transform transform, Vector2 target, float speed)
        {
            _transform = transform;
            _target = target;
            _speed = speed;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(Move(action));
        }
        
        private IEnumerator Move(UnityAction action)
        {
            var progress = 0f;

            do
            {
                _transform.position = Vector2.Lerp(_transform.position, _target, progress);
                yield return null;
                progress += Time.deltaTime * _speed;
            } while (progress < 1f);
            
            action.Invoke();
        }
    }
}