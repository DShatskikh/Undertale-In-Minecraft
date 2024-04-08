using System;
using Super_Auto_Mobs;
using UnityEngine;

namespace Game
{
    public class HatPoint: MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private SpriteRenderer _hatSpriteRenderer;
        
        [SerializeField] 
        private float _x1, _x2;

        private void Start()
        {
            if (GameData.IsHat)
            {
                Show();
            }
        }

        private void Update()
        {
            if (_hatSpriteRenderer.gameObject.activeSelf)
                transform.localPosition = transform.localPosition.SetX(_spriteRenderer.flipX ? _x2 : _x1);
        }

        public void Show()
        {
            _hatSpriteRenderer.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _hatSpriteRenderer.gameObject.SetActive(false);
        }
    }
}