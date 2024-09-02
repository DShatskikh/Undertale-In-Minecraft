using UnityEngine;

namespace Game
{
    public class DialogModel
    {
        public readonly ReactiveProperty<string> Text = new ReactiveProperty<string>();
        public readonly ReactiveProperty<bool> IsEndWrite = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> IsShowed = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<Sprite> Icon = new ReactiveProperty<Sprite>();
    }
}