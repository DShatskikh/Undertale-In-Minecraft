using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ChangeAlphaCommand : AwaitCommand
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly float _alpha;
        private readonly float _duration;

        public ChangeAlphaCommand(SpriteRenderer spriteRenderer, float alpha, float duration)
        {
            _spriteRenderer = spriteRenderer;
            _alpha = alpha;
            _duration = duration;
        }
        
        public override void Execute(UnityAction action)
        {
            GameData.CoroutineRunner.StartCoroutine(AwaitExecute(action));
        }

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            var progress = 0f;
            
            if (!_spriteRenderer)
                yield break;
            
            var startA = _spriteRenderer.color.a;

            while (progress < 1)
            {
                progress += Time.deltaTime / _duration;
                
                if (!_spriteRenderer)
                    yield break;
                
                _spriteRenderer.color = _spriteRenderer.color.SetA(Mathf.Lerp(startA, _alpha, progress));
                yield return null;
            }
            
            action?.Invoke();
        }
    }
}