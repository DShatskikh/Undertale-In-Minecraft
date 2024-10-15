using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class KeyButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private MMF_Player _pressedPlayer, _notPressedPlayer;
        
        private SettingKeyScreen _settingKeyScreen;
        private KeySlotViewModel _viewModel;

        public void Init(SettingKeyScreen settingKeyScreen, KeySlotViewModel viewModel)
        {
            _settingKeyScreen = settingKeyScreen;
            _viewModel = viewModel;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _settingKeyScreen.SelectSlot(_viewModel);
            _settingKeyScreen.SetIsRight(false);
            _settingKeyScreen.SetIsRight(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressedPlayer.PlayFeedbacks();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _notPressedPlayer.PlayFeedbacks();
            _settingKeyScreen.OnSubmitUp();
        }
    }
}