using UnityEngine;

namespace Game
{
    public abstract class UseObject : MonoBehaviour, IUseObject
    {
        public abstract void Use();
    }
}