using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class AnimatedButtonOld : ButtonBase, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private MMF_Player _pressedMmfPlayer;
        
        [SerializeField]
        private MMF_Player _notPressedMmfPlayer;

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Transform _view;

        [SerializeField]
        private Image _icon;

        private void OnDisable()
        {
            _pressedMmfPlayer.StopFeedbacks();
            _notPressedMmfPlayer.StopFeedbacks();
            
            _label.color = Color.white;
            _icon.color = Color.white;
            _view.transform.localScale = Vector3.one;
        }
        
        public override void OnClick() { }
        
        public void OnPointerDown(PointerEventData eventData) => 
            Down();

        public void OnPointerUp(PointerEventData eventData) => 
            Up();

        private void Down()
        {
            _pressedMmfPlayer.PlayFeedbacks();
            _notPressedMmfPlayer.StopFeedbacks();

            _label.color = GameData.AssetProvider.SelectColor;
            _icon.color = GameData.AssetProvider.SelectColor;
        }

        private void Up()
        {
            _pressedMmfPlayer.StopFeedbacks();
            _notPressedMmfPlayer.PlayFeedbacks();

            _label.color = Color.white;
            _icon.color = Color.white;
        }
    }
}