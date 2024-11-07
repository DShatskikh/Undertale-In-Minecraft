using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class RotationCommand : AwaitCommand
    {
        private readonly Transform _transform;
        private readonly Vector3 _angle;
        private readonly float _duration;

        public RotationCommand(Transform transform, Vector3 angle, float duration)
        {
            _transform = transform;
            _angle = angle;
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
            var startPosition = _transform.rotation.eulerAngles;

            do
            {
                if (_transform)
                    _transform.eulerAngles = Vector3.Lerp(startPosition, _angle, progress);
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