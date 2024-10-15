using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class AllResetKeySlotView : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _frame, _icon;

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Sprite _defaultIcon;

        [SerializeField]
        private MMF_Player _pressedPlayer;

        [SerializeField]
        private Transform _viewContainer;
        
        private AllResetKeySlotViewModel _viewModel;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.Select();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _viewModel.SubmitDown();
            _pressedPlayer.PlayFeedbacks();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _viewModel.Use();
            _viewContainer.localScale = Vector3.one;
        }

        public void Init(AllResetKeySlotViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Upgrade(bool isSelect)
        {
            if (isSelect)
            {
                _frame.color = GameData.AssetProvider.SelectColor;
                _label.color = GameData.AssetProvider.SelectColor;
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
            }
            else
            {
                _frame.color = GameData.AssetProvider.DeselectColor;
                _label.color = GameData.AssetProvider.DeselectColor;
                _icon.sprite = _defaultIcon;
            }
        }
    }
}