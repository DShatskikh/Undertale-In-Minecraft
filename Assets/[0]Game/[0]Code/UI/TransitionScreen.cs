using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class TransitionScreen : MonoBehaviour
    {
        private Image _image;
        private Sequence _animation;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public IEnumerator AwaitShow()
        {
            gameObject.SetActive(true);
            _image.color = _image.color.SetA(0);
            _animation = DOTween.Sequence();
            yield return _animation.Append(_image.DOColor(_image.color.SetA(1), 0.5f)).WaitForCompletion();
        }
        
        public IEnumerator AwaitHide()
        {
            _image.color = _image.color.SetA(1);
            _animation = DOTween.Sequence();
            yield return _animation.Append(_image.DOColor(_image.color.SetA(0), 0.5f)).WaitForCompletion();
            gameObject.SetActive(false);
        }
    }
}