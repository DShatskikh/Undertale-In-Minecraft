using PixelCrushers.DialogueSystem;
using UnityEngine;
using YG;

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
        private SpriteRenderer _freak;
        
        [SerializeField]
        private SpriteRenderer _cylinder;
        
        [SerializeField] 
        private float _x1, _x2;

        private void Start()
        {
            HatShow(YandexGame.savesData.IsCake);
            MaskShow(YandexGame.savesData.IsCheat);
            FreakShow(Lua.IsTrue("Variable[\"KILLS\"] >= 4"));
            CylinderShow(Lua.IsTrue("Variable[\"IsUseCylinder\"] == true"));
        }

        private void Update()
        {
            //transform.localPosition = transform.localPosition.SetX(_spriteRenderer.flipX ? _x2 : _x1);
            transform.localScale = transform.localScale.SetX(_spriteRenderer.flipX ? -1 : 1);
        }

        public void HatShow(bool isShow)
        {
            _hatSpriteRenderer.gameObject.SetActive(isShow);
        }

        public void MaskShow(bool isShow)
        {
            _hackerMask.gameObject.SetActive(isShow);
        }
        
        public void FreakShow(bool isShow)
        {
            _freak.gameObject.SetActive(isShow);
        }
        
        public void CylinderShow(bool isShow)
        {
            _cylinder.gameObject.SetActive(isShow);
        }
    }
}