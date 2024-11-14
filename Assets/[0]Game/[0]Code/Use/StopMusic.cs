using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class StopMusic : MonoBehaviour
    {
        private void OnEnable()
        {
            //StartCoroutine(AwaitStop());
        }

        private void Start()
        {
            Stop();
        }

        public void Use()
        {
            //StartCoroutine(AwaitStop());
        }

        public void Stop()
        {
            GameData.MusicPlayer.Stop();
        }
        
        private IEnumerator AwaitStop()
        {
            Stop();
            yield return null;
        }
    }
}