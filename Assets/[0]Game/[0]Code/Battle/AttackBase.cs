using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class AttackBase : MonoBehaviour
    {
        public abstract void Execute(UnityAction action);
    }
}