using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameplayMenuSlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon, _frame;
        
        [SerializeField]
        private Image _selectIcon;

        [SerializeField]
        private MMF_Player _selectPlayer;
        
        public void Init(GameplayMenuSlotModel model)
        {
            _icon.sprite = model.Config.Icon;
        }

        public void Upgrade(bool isSelect, GameplayMenuSlotModel model)
        {
            if (isSelect)
            {
                _selectIcon.gameObject.SetActive(true);
                _icon.color = GameData.AssetProvider.SelectColor;
                _frame.color = GameData.AssetProvider.SelectColor;
                
                _selectPlayer.PlayFeedbacks();
            }
            else
            {
                _selectIcon.gameObject.SetActive(false);
                _icon.color = GameData.AssetProvider.DeselectColor;
                _frame.color = GameData.AssetProvider.DeselectColor;
            }
        }
    }
}