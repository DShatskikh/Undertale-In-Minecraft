namespace Game
{
    public class SelectModel
    {
        public readonly ReactiveProperty<string> Text = new ReactiveProperty<string>();
        public readonly ReactiveProperty<string> YesResultString = new ReactiveProperty<string>();
        public readonly ReactiveProperty<string> NoResultString = new ReactiveProperty<string>();
        public readonly ReactiveProperty<bool> IsEndWrite = new ReactiveProperty<bool>();
    }
}