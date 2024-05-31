using UnityEngine;

namespace Game
{
    public abstract class UseObject : MonoBehaviour
    {
        public Vector2 Offset;
        public abstract void Use();
    }
}