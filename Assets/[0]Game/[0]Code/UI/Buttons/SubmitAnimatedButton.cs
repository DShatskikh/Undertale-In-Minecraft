namespace Game
{
    public class SubmitAnimatedButton : AnimatedButton
    {
        protected override void Enable()
        {
            EventBus.Submit += _view.Down;
            EventBus.SubmitUp += _view.Up;
            EventBus.SubmitUp += _button.onClick.Invoke;
        }

        protected override void Disable()
        {
            EventBus.Submit -= _view.Down;
            EventBus.SubmitUp -= _view.Up;
            EventBus.SubmitUp -= _button.onClick.Invoke;
        }
    }
}