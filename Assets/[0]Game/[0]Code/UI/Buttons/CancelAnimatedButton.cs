namespace Game
{
    public class CancelAnimatedButton : AnimatedButton
    {
        protected override void Enable()
        {
            EventBus.Cancel += _view.Down;
            EventBus.CancelUp += _view.Up;
            EventBus.CancelUp += _button.onClick.Invoke;
        }

        protected override void Disable()
        {
            EventBus.Cancel -= _view.Down;
            EventBus.CancelUp -= _view.Up;
            EventBus.CancelUp -= _button.onClick.Invoke;
        }
    }
}