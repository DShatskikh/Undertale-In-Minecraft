using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class KeySlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        
        [SerializeField]
        private TMP_Text _label, _keyLabel;

        [SerializeField]
        private Sprite _iconSprite;
        
        private KeySlotViewModel _viewModel;
        private string _keyHash;

        public void Init(KeySlotViewModel viewModel, string keyHash)
        {
            _viewModel = viewModel;
            _keyHash = keyHash;

            _keyLabel.text = "[Z]";
        }

        public void SetSelect(bool isSelect)
        {
            if (isSelect)
            {
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
                _label.color = GameData.AssetProvider.SelectColor;
            }
            else
            {
                _icon.sprite = _iconSprite;
                _label.color = GameData.AssetProvider.DeselectColor;
            }
        }
    }
}