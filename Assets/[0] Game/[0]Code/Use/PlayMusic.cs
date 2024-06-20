using System.Collections;
using UnityEngine;

namespace Game
{
    public class PlayMusic : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _music;

        private Coroutine _coroutine;
        
        private void OnEnable()
        {
            if (_coroutine != null) 
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(AwaitPlay());
        }

        public void Play()
        {
            GameData.MusicAudioSource.clip = _music;
            GameData.MusicAudioSource.Play();
        }

        private IEnumerator AwaitPlay()
        {
            yield return null;
            Play();
        }
    }
}