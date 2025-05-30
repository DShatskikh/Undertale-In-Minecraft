using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ScaleCommand : AwaitCommand
    {
        private readonly Transform _transform;
        private readonly Vector2 _target;
        private readonly float _duration;

        public ScaleCommand(Transform transform, Vector2 target, float duration)
        {
            _transform = transform;
            _target = target;
            _duration = duration;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitExecute(action));
        }

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            if (_transform == null)
            {
                action.Invoke();
                yield break;
            }
            
            var progress = 0f;
            var startScale= _transform.localScale;

            do
            {
                if (_transform)
                    _transform.localScale = Vector2.Lerp(startScale, _target, progress);
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