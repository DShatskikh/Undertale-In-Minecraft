using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class LanguageSlotView : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _icon, _bracketL, _bracketR, _flag;

        [SerializeField]
        private TMP_Text _label, _languageLabel;

        [SerializeField]
        private Sprite[] _flags;
        
        private LanguageSlotViewModel _viewModel;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.Select();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _viewModel.Click();
        }

        public void Init(LanguageSlotViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnEnable()
        {
            YandexGame.savesData.SettingData.Language.Changed += LanguageOnChanged;
            LanguageOnChanged(YandexGame.savesData.SettingData.Language.Value);
        }

        private void OnDisable()
        {
            YandexGame.savesData.SettingData.Language.Changed -= LanguageOnChanged;
        }
        
        private void LanguageOnChanged(int value)
        {
            _flag.sprite = _flags[value];
            _languageLabel.text = value == 0 ? "Русский" : "English";
        }

        public void Upgrade(bool isSelect, bool isShow)
        {
            if (isSelect)
            {
                _icon.sprite = GameData.AssetProvider.CharacterIcon;

                if (isShow)
                {
                    _bracketL.color = GameData.AssetProvider.DeselectColor;
                    _bracketR.color = GameData.AssetProvider.DeselectColor;
                    _languageLabel.color = GameData.AssetProvider.DeselectColor;
                    _label.color = GameData.AssetProvider.DeselectColor;
                }
                else
                {
                    _bracketL.color = GameData.AssetProvider.SelectColor;
                    _bracketR.color = GameData.AssetProvider.SelectColor;
                    _languageLabel.color = GameData.AssetProvider.SelectColor;  
                    _label.color = GameData.AssetProvider.SelectColor;
                }
            }
            else
            {
                _label.color = GameData.AssetProvider.DeselectColor;
                _bracketL.color = GameData.AssetProvider.DeselectColor;
                _bracketR.color = GameData.AssetProvider.DeselectColor;
                _languageLabel.color = GameData.AssetProvider.DeselectColor;
            }
        }
    }
}