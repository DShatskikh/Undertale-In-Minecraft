namespace Game
{
    public class MonologModel
    {
        public readonly ReactiveProperty<string> Text = new();
        public readonly ReactiveProperty<bool> IsEndWrite = new();
    }
}