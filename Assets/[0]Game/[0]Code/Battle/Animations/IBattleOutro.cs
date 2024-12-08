using System;
using System.Collections;
using UnityEngine.Events;

namespace Game
{
    public interface IBattleOutro
    {
        IEnumerator AwaitOutro();
    }
}