using System;

namespace Game
{
    public class ReactiveProperty<T>
    {
        public event Action<T> Changed;

        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed?.Invoke(value);
            }
    }
    }
}