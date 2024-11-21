using System.Collections.Generic;
using NUnit.Framework;
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
        private SpriteRenderer _mysteryCylinder;
        
        [SerializeField]
        private SpriteRenderer _eliteCylinder;

        [SerializeField]
        private Transform[] _points;
        
        [SerializeField] 
        private float _x1, _x2;

        private void Start()
        {
            UpgradeView();
        }

        private void Update()
        {
            //transform.localPosition = transform.localPosition.SetX(_spriteRenderer.flipX ? _x2 : _x1);
            transform.localScale = transform.localScale.SetX(_spriteRenderer.flipX ? -1 : 1);
        }

        public void UpgradeView()
        {
            HatShow(YandexGame.savesData.IsCake);
            MaskShow(YandexGame.savesData.IsCheat);
            FreakShow(Lua.IsTrue("Variable[\"KILLS\"] >= 4"));
            CylinderShow(Lua.IsTrue("Variable[\"IsUseCylinder\"] == true"));
            MysticalCylinderShow(Lua.IsTrue("Variable[\"IsUseMysteryCylinder\"] == true"));
            EliteCylinderShow(Lua.IsTrue("Variable[\"IsUseEliteCylinder\"] == true"));

            var hats = new List<SpriteRenderer>() { _eliteCylinder, _cylinder, _mysteryCylinder };

            foreach (var point in _points)
            {
                foreach (var hat in hats)
                {
                    if (hat.gameObject.activeSelf)
                    {
                        hat.transform.position = point.position;
                        hat.transform.eulerAngles = point.eulerAngles;
                        hats.Remove(hat);
                        break;
                    }
                }
            }
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
        
        public void CylinderShow(bool isShow) => 
            _cylinder.gameObject.SetActive(isShow);
        
        public void MysticalCylinderShow(bool isShow) => 
            _mysteryCylinder.gameObject.SetActive(isShow);
        
        public void EliteCylinderShow(bool isShow) => 
            _eliteCylinder.gameObject.SetActive(isShow);
    }
}