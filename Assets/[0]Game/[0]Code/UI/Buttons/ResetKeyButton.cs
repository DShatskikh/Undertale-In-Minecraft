using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class ResetKeyButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
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
            _settingKeyScreen.SetIsRight(true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressedPlayer.PlayFeedbacks();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _notPressedPlayer.PlayFeedbacks();
            _settingKeyScreen.OnSubmitUp();
            _settingKeyScreen.SetIsRight(true);
        }
    }
}