using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class PlusMinusVolumeView : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField]
        private VolumeSlotViewModel _viewModel;

        [SerializeField]
        private Image _frame;

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private MMF_Player _pressedPlayer, _notPressedPlayer;

        [SerializeField]
        private bool _isPlus = true;
        
        private bool _isPressed;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _frame.color = GameData.AssetProvider.SelectColor;
            _label.color = GameData.AssetProvider.SelectColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _frame.color = Color.white;
            _label.color = Color.white;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
            _pressedPlayer.PlayFeedbacks();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
            _notPressedPlayer.PlayFeedbacks();
        }

        private void Update()
        {
            if (_isPressed)
                _viewModel.AddVolume(_isPlus ? Time.deltaTime : -Time.deltaTime);
        }
    }
}