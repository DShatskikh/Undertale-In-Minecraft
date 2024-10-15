using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class GameplayMenuSlotView : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _icon, _frame;
        
        [SerializeField]
        private Image _selectIcon;

        [SerializeField]
        private MMF_Player _selectPlayer;

        private GameplayMenuSlotViewModel _viewModel;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.Select();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _viewModel.SubmitUp();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _viewModel.Use(); 
        }
        
        public void Init(GameplayMenuSlotModel model, GameplayMenuSlotViewModel viewModel)
        {
            _icon.sprite = model.Config.Icon;
            _viewModel = viewModel;
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