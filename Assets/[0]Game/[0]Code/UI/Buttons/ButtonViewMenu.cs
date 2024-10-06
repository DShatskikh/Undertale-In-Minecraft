using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ButtonViewMenu : ButtonViewBase
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

        public override void Disable()
        {
            _pressedMmfPlayer.StopFeedbacks();
            _notPressedMmfPlayer.StopFeedbacks();
            
            _label.color = Color.white;
            _icon.color = Color.white;
            _view.transform.localScale = Vector3.one;
        }

        public override void Down()
        {
            _pressedMmfPlayer.PlayFeedbacks();
            _notPressedMmfPlayer.StopFeedbacks();

            _label.color = GameData.AssetProvider.SelectColor;
            _icon.color = GameData.AssetProvider.SelectColor;
        }

        public override void Up()
        {
            _pressedMmfPlayer.StopFeedbacks();
            _notPressedMmfPlayer.PlayFeedbacks();

            _label.color = Color.white;
            _icon.color = Color.white;
        }
    }
}