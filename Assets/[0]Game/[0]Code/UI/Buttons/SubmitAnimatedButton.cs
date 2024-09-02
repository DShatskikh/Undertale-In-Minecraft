namespace Game
{
    public class SubmitAnimatedButton : DialogueButton
    {
        public override void Enable()
        {
            EventBus.Submit += Down;
            EventBus.SubmitUp += Up;
            EventBus.SubmitUp += _button.onClick.Invoke;
        }

        public override void Disable()
        {
            EventBus.Submit -= Down;
            EventBus.SubmitUp -= Up;
            EventBus.SubmitUp -= _button.onClick.Invoke;
        }
    }
}