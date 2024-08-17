using UnityEngine;

namespace Game
{
    public class PlaySoundEffect : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _effect;
        
        public void Play()
        {
            GameData.EffectSoundPlayer.Play(_effect);
        }
    }
}