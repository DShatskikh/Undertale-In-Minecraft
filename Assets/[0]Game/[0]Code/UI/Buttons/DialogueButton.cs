using MoreMountains.Feedbacks;
using RimuruDev;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public abstract class DialogueButton : ButtonBase, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private MMF_Player _pressedMmfPlayer;
        
        [SerializeField]
        private MMF_Player _notPressedMmfPlayer;

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private TMP_Text _hint;

        [SerializeField]
        private Transform _view;

        [SerializeField]
        private Image _viewImage;
        
        private void Start()
        {
            if (GameData.DeviceType == CurrentDeviceType.WebMobile)
                _hint.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Enable();
        }

        private void OnDisable()
        {
            _pressedMmfPlayer.StopFeedbacks();
            _notPressedMmfPlayer.StopFeedbacks();
            
            _label.color = GameData.AssetProvider.DeselectColor;
            _viewImage.color = GameData.AssetProvider.DeselectColor;
            _view.transform.localScale = Vector3.one;
            
            Disable();
        }

        public abstract void Enable();
        public abstract void Disable();
        public override void OnClick() { }
        
        public void OnPointerDown(PointerEventData eventData) => 
            Down();

        public void OnPointerUp(PointerEventData eventData) => 
            Up();

        protected void Down()
        {
            _pressedMmfPlayer.PlayFeedbacks();
            _notPressedMmfPlayer.StopFeedbacks();

            _viewImage.color = GameData.AssetProvider.SelectColor;
            _label.color = GameData.AssetProvider.SelectColor;
            
            print("Down");
        }
        
        protected void Up()
        {
            _pressedMmfPlayer.StopFeedbacks();
            _notPressedMmfPlayer.PlayFeedbacks();
            
            _viewImage.color = GameData.AssetProvider.DeselectColor;
            _label.color = GameData.AssetProvider.DeselectColor;
            
            print("Up");
        }
    }
}