using System;
using Super_Auto_Mobs;
using UnityEngine;

namespace Game
{
    public class HackerMask : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            UpgradeView();
        }

        private void Update()
        {
            UpgradeView();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void UpgradeView()
        {
            _spriteRenderer.flipX = GameData.Character.View.IsFlip;
            transform.localPosition = transform.localPosition.SetX(_spriteRenderer.flipX ? -0.153f : 0.153f);
        }
    }
}