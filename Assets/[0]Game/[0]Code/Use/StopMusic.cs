using System.Collections;
using UnityEngine;

namespace Game
{
    public class StopMusic : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(AwaitStop());
        }

        public void Use()
        {
            StartCoroutine(AwaitStop());
        }

        public void Stop()
        {
            GameData.MusicAudioSource.Stop();
        }
        
        private IEnumerator AwaitStop()
        {
            yield return null;
            Stop();
        }
    }
}