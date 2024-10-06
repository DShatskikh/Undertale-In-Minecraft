using UnityEngine;

namespace Game
{
    public abstract class ButtonViewBase : MonoBehaviour
    {
        public abstract void Disable();
        public abstract void Down();
        public abstract void Up();
    }
}