using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class ChangeAlphaImageCommand : AwaitCommand
    {
        private readonly Image _image;
        private readonly float _alpha;
        private readonly float _duration;

        public ChangeAlphaImageCommand(Image image, float alpha, float duration)
        {
            _image = image;
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
            var startA = _image.color.a;

            while (progress < 1)
            {
                progress += Time.deltaTime / _duration;
                
                if (!_image)
                    yield break;
                
                _image.color = _image.color.SetA(Mathf.Lerp(startA, _alpha, progress));
                yield return null;
            }
            
            action.Invoke();
        }
    }
}