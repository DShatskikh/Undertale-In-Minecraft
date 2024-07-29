using System;
using UnityEngine;
using YG;

namespace Game
{
    public class HerobrinePicture : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] _sprites;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            if (!YandexGame.savesData.IsAllEnd)
                return;
            
            _spriteRenderer.sprite = (YandexGame.savesData.NumberGame % _sprites.Length) switch
            {
                2 => _sprites[0],
                3 => _sprites[1],
                _ => _spriteRenderer.sprite
            };
        }
    }
}