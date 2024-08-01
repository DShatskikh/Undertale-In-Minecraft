using System;
using UnityEngine;

namespace Game
{
    public class Lamp : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public void Activate(bool isOn)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = isOn ? Color.white : new Color(0.2f, 0.2f, 0.2f);
        }
    }
}