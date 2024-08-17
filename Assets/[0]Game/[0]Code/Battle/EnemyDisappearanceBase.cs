using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class EnemyDisappearanceBase : MonoBehaviour
    {
        public abstract void Disappearance(UnityAction action);
    }
}