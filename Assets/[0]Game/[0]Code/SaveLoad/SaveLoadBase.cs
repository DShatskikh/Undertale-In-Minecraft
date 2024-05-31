using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{
    public abstract class SaveLoadBase : MonoBehaviour
    {
        public abstract void Load();

        public abstract void Reset();
    }
}