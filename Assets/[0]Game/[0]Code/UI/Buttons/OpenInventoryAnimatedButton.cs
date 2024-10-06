namespace Game
{
    public class OpenInventoryAnimatedButton : AnimatedButton
    {
        protected override void Enable()
        {
            EventBus.OpenInventory += _view.Down;
            EventBus.OpenInventoryUp += _view.Up;
            EventBus.OpenInventoryUp += _button.onClick.Invoke;
        }

        protected override void Disable()
        {
            EventBus.OpenInventory -= _view.Down;
            EventBus.OpenInventoryUp -= _view.Up;
            EventBus.OpenInventoryUp -= _button.onClick.Invoke;
        }
        
        public override void OnClick()
        {

        }
    }
}