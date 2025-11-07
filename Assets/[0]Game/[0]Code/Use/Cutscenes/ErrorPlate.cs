using UnityEngine;

namespace Game
{
    public sealed class ErrorPlate : MonoBehaviour
    {
        [SerializeField] 
        private Sprite _activeSprite;

        [SerializeField] 
        private Sprite _deactivateSprite;

        [SerializeField]
        private AudioClip _sfx;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character character))
            {
                GetComponent<SpriteRenderer>().sprite = _activeSprite;
                GameData.EffectAudioSource.clip = _sfx;
                GameData.EffectAudioSource.Play();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character character))
            {
                GetComponent<SpriteRenderer>().sprite = _deactivateSprite;
            }
        }
    }
}