using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ChangeAlphaTextCommand : AwaitCommand
    {
        private readonly TMP_Text _text;
        private readonly float _alpha;
        private readonly float _duration;

        public ChangeAlphaTextCommand(TMP_Text text, float alpha, float duration)
        {
            _text = text;
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
            var startA = _text.color.a;

            while (progress < 1)
            {
                progress += Time.deltaTime / _duration;
                
                if (!_text)
                    yield break;
                
                _text.color = _text.color.SetA(Mathf.Lerp(startA, _alpha, progress));
                yield return null;
            }
            
            action.Invoke();
        }
    }
}