using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace Game
{
    public class Chest : UseObject
    {
        [SerializeField]
        private SaveKeyBool _key;

        [SerializeField]
        private Sprite _openSprite;
        
        [SerializeField]
        private AudioClip _effect;
        
        [SerializeField]
        private LocalizedString _selectString;

        [SerializeField]
        private OpenMonolog _pickMonolog;

        [SerializeField]
        private OpenMonolog _notPickMonolog;

        [FormerlySerializedAs("_openMonolog")] [SerializeField]
        private OpenMonolog _openChestMonolog;

        private bool _isOpen;
        
        private void Start()
        {
            if (GameData.Saver.LoadKey(_key))
                Open();
        }

        public override void Use()
        {
            if (!_isOpen)
                GameData.Select.Show(_selectString.GetLocalizedString(), Pick, NotPick);
            else
                _openChestMonolog.Use();
        }

        private void Pick()
        {
            GameData.EffectAudioSource.clip = _effect;
            GameData.EffectAudioSource.Play();
            
            _pickMonolog.Use();
            Open();
        }
        
        private void NotPick()
        {
            _notPickMonolog.Use();
        }
        
        private void Open()
        {
            _isOpen = true;
            GetComponent<SpriteRenderer>().sprite = _openSprite;
        }
    }
}