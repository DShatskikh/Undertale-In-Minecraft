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
            var source = GameData.MusicAudioSource;
            
            if (source.clip == _music && source.isPlaying)
                return;
            
            source.clip = _music;
            source.Play();
        }

        private IEnumerator AwaitPlay()
        {
            yield return null;
            Play();
        }
    }
}