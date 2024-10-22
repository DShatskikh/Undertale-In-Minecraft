using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SpeakAtcSlot : BaseSlotController
    {
        [SerializeField]
        private Image _bracketL;
        
        [SerializeField]
        private Image _bracketR;

        [SerializeField]
        private Image _icon;

        public Sprite GetIcon => _icon.sprite;

        public void Init(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public override void SetSelected(bool isSelect)
        {
            if (isSelect)
            {
                _icon.color = GameData.AssetProvider.SelectColor;
                _bracketL.gameObject.SetActive(true);
                _bracketR.gameObject.SetActive(true);
            }
            else
            {
                _icon.color = Color.white;
                _bracketL.gameObject.SetActive(false);
                _bracketR.gameObject.SetActive(false);
            }
        }
    }
}