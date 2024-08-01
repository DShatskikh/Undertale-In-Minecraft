using System;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Game
{
    public class Lever : UseObject
    {
        [SerializeField]
        private Sprite _onSprite, _offSprite;

        private bool _isOn;
        private SpriteRenderer _spriteRenderer;

        public UnityAction<Lever, bool> Used;
        public bool IsOn => _isOn;

        public void Init(bool isActivated)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _isOn = isActivated;
            ViewUpgrade();
        }

        public override void Use()
        {
            _isOn = !_isOn;
            Used.Invoke(this, _isOn);
            ViewUpgrade();
        }

        private void ViewUpgrade()
        {
            _spriteRenderer.sprite = _isOn ? _onSprite : _offSprite;
        }
    }
}