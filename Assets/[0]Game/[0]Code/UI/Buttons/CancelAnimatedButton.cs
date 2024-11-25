namespace Game
{
    public class CancelAnimatedButton : AnimatedButton
    {
        private bool _isDown;
        
        protected override void Enable()
        {
            EventBus.Cancel += OnSubmitDown;
            EventBus.CancelUp += OnSubmitUp;
        }

        protected override void Disable()
        {
            EventBus.Cancel -= OnSubmitDown;
            EventBus.CancelUp -= OnSubmitUp;
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
            EventBus.CancelUp?.Invoke();
        }
    }
}