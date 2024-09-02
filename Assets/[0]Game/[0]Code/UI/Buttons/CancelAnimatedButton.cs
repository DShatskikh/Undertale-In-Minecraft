namespace Game
{
    public class CancelAnimatedButton : DialogueButton
    {
        public override void Enable()
        {
            EventBus.Cancel += Down;
            EventBus.CancelUp += Up;
            EventBus.CancelUp += _button.onClick.Invoke;
        }

        public override void Disable()
        {
            EventBus.Cancel -= Down;
            EventBus.CancelUp -= Up;
            EventBus.CancelUp -= _button.onClick.Invoke;
        }
    }
}