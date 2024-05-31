using System.Collections;
using UnityEngine;

namespace Game
{
    public class StopMusic : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(Stop());
        }

        private IEnumerator Stop()
        {
            yield return null;
            GameData.MusicAudioSource.Stop();
        }
    }
}