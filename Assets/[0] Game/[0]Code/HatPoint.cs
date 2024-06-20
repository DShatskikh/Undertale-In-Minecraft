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
        private SpriteRenderer _hackerMask;
        
        [SerializeField] 
        private float _x1, _x2;

        private void Start()
        {
            HatShowAndHide(GameData.IsHat);
            MaskShowAndHide(GameData.IsCheat);
        }

        private void Update()
        {
            //transform.localPosition = transform.localPosition.SetX(_spriteRenderer.flipX ? _x2 : _x1);
            transform.localScale = transform.localScale.SetX(_spriteRenderer.flipX ? -1 : 1);
        }

        public void HatShowAndHide(bool isShow)
        {
            _hatSpriteRenderer.gameObject.SetActive(isShow);
        }

        public void MaskShowAndHide(bool isShow)
        {
            _hackerMask.gameObject.SetActive(isShow);
        }
    }
}