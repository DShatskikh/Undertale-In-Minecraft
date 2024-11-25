using System.Collections;

namespace Game
{
    public class SubmitAnimatedButton : AnimatedButton
    {
        private bool _isDown;
        
        protected override void Enable()
        {
            EventBus.Submit += OnSubmitDown;
            EventBus.SubmitUp += OnSubmitUp;

            //StartCoroutine(AwaitCheck());
        }

        protected override void Disable()
        {
            EventBus.Submit -= OnSubmitDown;
            EventBus.SubmitUp -= OnSubmitUp;
        }

        private void OnSubmitDown()
        {
            _isDown = true;
            _view.Down();
        }

        private void OnSubmitUp()
        {
            if (!_isDown)
                return;

            _isDown = false;
            _view.Up();
            _button.onClick.Invoke();
        }

        public override void OnClick()
        {
            Disable();
            EventBus.SubmitUp?.Invoke();
        }
        
        private IEnumerator AwaitCheck()
        {
            while (true)
            {
                yield return null;
                
                if (EventBus.Submit == _view.Down)
                    gameObject.SetActive(false);
                
                print(EventBus.Submit);
            }
        }
    }
}