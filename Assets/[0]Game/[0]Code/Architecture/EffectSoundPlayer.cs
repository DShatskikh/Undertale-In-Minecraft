using UnityEngine;

namespace Game
{
    public class EffectSoundPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip Clip => _audioSource.clip;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        [ContextMenu("TestPlay")]
        private void TestPlay()
        {
            Play(GameData.AssetProvider.ClickSound);
        }
    }
}