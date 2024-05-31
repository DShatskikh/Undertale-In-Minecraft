using UnityEngine;

namespace Game
{
    public class PlaySoundEffect : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _effect;
        
        public void Play()
        {
            GameData.EffectAudioSource.clip = _effect;
            GameData.EffectAudioSource.Play();
        }
    }
}