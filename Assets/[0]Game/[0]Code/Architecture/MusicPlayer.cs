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

        public void Play(AudioClip clip)
        {
            if (_audioSource.clip == clip && _audioSource.isPlaying)
                return;

            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }
    }
}