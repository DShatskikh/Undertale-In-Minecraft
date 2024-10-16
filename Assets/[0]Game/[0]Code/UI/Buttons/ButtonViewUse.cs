using MoreMountains.Feedbacks;
using RimuruDev;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ButtonViewUse : ButtonViewBase
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
            if (GameData.DeviceType == CurrentDeviceType.Mobile)
                _hint.gameObject.SetActive(false);
        }

        public override void Disable()
        {
            _pressedMmfPlayer.StopFeedbacks();
            _notPressedMmfPlayer.StopFeedbacks();
            
            _label.color = GameData.AssetProvider.DeselectColor;
            _viewImage.color = GameData.AssetProvider.DeselectColor;
            _view.transform.localScale = Vector3.one;
        }

        public override void Down()
        {
            _pressedMmfPlayer.PlayFeedbacks();
            _notPressedMmfPlayer.StopFeedbacks();

            _viewImage.color = GameData.AssetProvider.SelectColor;
            _label.color = GameData.AssetProvider.SelectColor;
        }

        public override void Up()
        {
            _pressedMmfPlayer.StopFeedbacks();
            _notPressedMmfPlayer.PlayFeedbacks();
            
            _viewImage.color = GameData.AssetProvider.DeselectColor;
            _label.color = GameData.AssetProvider.DeselectColor;
            
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
        }
    }
}