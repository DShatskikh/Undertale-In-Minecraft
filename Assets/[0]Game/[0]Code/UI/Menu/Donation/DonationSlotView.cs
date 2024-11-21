using System.Collections;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using YG;
using YG.Utils.Pay;

namespace Game
{
    public class DonationSlotView : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        private ImageLoadYG _icon;
        
        [SerializeField]
        private Image _frame;
        
        [SerializeField]
        private Image _buttonFrame;
        
        [SerializeField]
        private TMP_Text _nameLabel;

        [SerializeField]
        private TMP_Text _descriptionLabel;
        
        [SerializeField]
        private TMP_Text _priceLabel;

        [SerializeField]
        private TMP_Text _purchasedLabel;
        
        [SerializeField]
        private SubmitAnimatedButton _button;

        private Purchase _model;
        private DonationSlotViewModel _viewModel;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.Select();
        }

        /*public void OnPointerDown(PointerEventData eventData)
        {
            _viewModel.SubmitDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _viewModel.Use(); 
        }*/
        
        public void Init(Purchase model, DonationSlotViewModel viewModel)
        {
            _model = model;
            _nameLabel.text = _model.title;
            _descriptionLabel.text = _model.description;
            _priceLabel.text = _model.priceValue + Yan();
            _viewModel = viewModel;

            _icon.Load(model.imageURI);
            
            if (Lua.IsTrue($"Variable[\"Is{_model.id}\"] == true"))
            {
                _button.gameObject.SetActive(false);
                _priceLabel.gameObject.SetActive(false);
                _purchasedLabel.gameObject.SetActive(true);
            }
            
            LocalizationSettings.SelectedLocaleChanged += LocalizationSettingsOnSelectedLocaleChanged;
        }
        
        private void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= LocalizationSettingsOnSelectedLocaleChanged;
        }

        private void LocalizationSettingsOnSelectedLocaleChanged(Locale obj)
        {
            
        }

        public void SubmitUp()
        {
            
        }
        
        public void SubmitDown()
        {
           
        }
        
        public void Upgrade(bool isSelect)
        {
            if (isSelect)
            {
                _frame.color = GameData.AssetProvider.SelectColor;
                _buttonFrame.color = GameData.AssetProvider.SelectColor;
                _button.enabled = true;
                _button.GetComponent<Button>().onClick.AddListener(() => _viewModel.Use());
            }
            else
            {
                _frame.color = Color.white;
                _buttonFrame.color = GameData.AssetProvider.DeselectColor;
                _button.enabled = false;
                _button.GetComponent<Button>().onClick.RemoveListener(() => _viewModel.Use());
            }
        }
        
        private string Yan()
        {
            if (YandexGame.savesData.language == "ru")
                return " Ян";
            else
                return " Yan";
        }
    }
}