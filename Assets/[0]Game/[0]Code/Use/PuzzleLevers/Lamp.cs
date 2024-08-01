using System;
using UnityEngine;

namespace Game
{
    public class Lamp : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Activate(bool isOn)
        {
            _spriteRenderer.color = isOn ? Color.white : new Color(0.2f, 0.2f, 0.2f);
        }
    }
}