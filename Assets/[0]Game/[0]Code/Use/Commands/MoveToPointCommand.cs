using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class MoveToPointCommand : AwaitCommand
    {
        private readonly Transform _transform;
        private readonly Vector2 _target;
        private readonly float _duration;
        
        public MoveToPointCommand(Transform transform, Vector2 target, float duration)
        {
            _transform = transform;
            _target = target;
            _duration = duration;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(Move(action));
        }

        public override IEnumerator Await()
        {
            Execute(_action);
            yield return new WaitUntil(() => _isAction);
        }

        private IEnumerator Move(UnityAction action)
        {
            var progress = 0f;
            var startPosition = _transform.position;

            do
            {
                if (_transform)
                    _transform.position = Vector2.Lerp(startPosition, _target, progress);
                else
                {
                    action.Invoke();
                    yield break;
                }

                yield return null;
                progress += Time.deltaTime / _duration;
            } while (progress < 1f);
            
            action.Invoke();
        }
    }
}