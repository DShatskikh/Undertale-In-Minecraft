using UnityEngine;

namespace Game
{
    public class MusicPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip Clip => _audioSource.clip;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip, float time = 0)
        {
            if (_audioSource.clip == clip && _audioSource.isPlaying)
                return;

            _audioSource.clip = clip;

            if (time != 0)
                _audioSource.time = time;
            
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        public float GetTime() => 
            _audioSource.time;
    }
}