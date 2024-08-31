namespace Game
{
    public class SelectModel
    {
        public ReactiveProperty<string> Text = new ReactiveProperty<string>();
        public ReactiveProperty<string> YesResultString = new ReactiveProperty<string>();
        public ReactiveProperty<string> NoResultString = new ReactiveProperty<string>();
        public ReactiveProperty<bool> IsEndWrite = new ReactiveProperty<bool>();
    }
}