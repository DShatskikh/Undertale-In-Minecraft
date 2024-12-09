using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class SaveLoadBase : MonoBehaviour
    {
        [SerializeField]
        protected string _key;
        
        public abstract void Load();

        public abstract void Reset();
    }
}