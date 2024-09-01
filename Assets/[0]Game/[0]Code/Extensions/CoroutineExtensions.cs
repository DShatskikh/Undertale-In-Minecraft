using System;
using System.Collections;

namespace Game
{
    public static class CoroutineExtensions
    {
        public static IEnumerator ContinueWith(this IEnumerator enumerator, Action action)
        {
            while (enumerator.MoveNext())
                yield return enumerator.Current;

            action();
        }
    }
}