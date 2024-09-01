namespace Game
{
    public class MonologModel
    {
        public readonly ReactiveProperty<string> Text = new ReactiveProperty<string>();
        public readonly ReactiveProperty<bool> IsEndWrite = new ReactiveProperty<bool>();
    }
}