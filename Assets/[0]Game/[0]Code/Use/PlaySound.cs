using UnityEngine;

namespace Game
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _sound;

        public void Play()
        {
            GameData.EffectAudioSource.clip = _sound;
            GameData.EffectAudioSource.Play();
        }
    }
}