using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class TransparencyCommand : AwaitCommand
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly float _alpha;
        private readonly float _duration;

        public TransparencyCommand(SpriteRenderer spriteRenderer, float alpha, float duration)
        {
            _spriteRenderer = spriteRenderer;
            _alpha = alpha;
            _duration = duration;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.Startup.StartCoroutine(AwaitTransparency(action));
        }

        public override IEnumerator Await()
        {
            Execute(_action);
            yield return new WaitUntil(() => _isAction);
        }

        private IEnumerator AwaitTransparency(UnityAction action)
        {
            var progress = 0f;
            
            if (!_spriteRenderer)
                yield break;
            
            var startAlpha = _spriteRenderer.color.a;
            
            while (progress < 1)
            {
                progress += Time.deltaTime / _duration;
                
                if (_spriteRenderer)
                    _spriteRenderer.color = _spriteRenderer.color.SetA(Mathf.Lerp(startAlpha, _alpha, progress));
                else
                {
                    action.Invoke();
                    yield break;
                }
                
                yield return null;
            }
            
            action.Invoke();
        }
    }
}