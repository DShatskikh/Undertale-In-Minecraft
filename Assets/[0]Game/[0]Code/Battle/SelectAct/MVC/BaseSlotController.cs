using UnityEngine;

namespace Game
{
    public abstract class BaseSlotController : MonoBehaviour
    {
        public abstract void SetSelected(bool isSelect);

        public virtual void SubmitDown() { }
        public virtual void SubmitUp() { }
        public virtual void Select() { }
        public virtual void Use() { }
    }
}