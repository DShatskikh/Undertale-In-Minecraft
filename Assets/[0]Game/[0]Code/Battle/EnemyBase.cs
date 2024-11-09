using System.Collections;
using UnityEngine;

namespace Game
{
    public abstract class EnemyBase : MonoBehaviour
    {
        public abstract IEnumerator AwaitCustomEvent(string eventName);
    }
}