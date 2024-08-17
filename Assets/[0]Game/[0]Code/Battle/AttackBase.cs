using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class AttackBase : MonoBehaviour
    {
        public BattleMessageData[] Messages;
        
        public abstract void Execute(UnityAction action);
    }
}