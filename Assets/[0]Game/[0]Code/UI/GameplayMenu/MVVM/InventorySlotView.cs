using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Game
{
    public class InventorySlotView : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _label;

        private ItemConfig _model;
        private InventorySlotViewModel _viewModel;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.Select();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _viewModel.SubmitSlotDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _viewModel.SubmitSlotUp(); 
        }
        
        public void Init(ItemConfig model, InventorySlotViewModel viewModel)
        {
            _model = model;
            _icon.sprite = _model.Icon;
            _viewModel = viewModel;
            
            StartCoroutine(AwaitLoadText());
            LocalizationSettings.SelectedLocaleChanged += LocalizationSettingsOnSelectedLocaleChanged;
        }
        
        private void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= LocalizationSettingsOnSelectedLocaleChanged;
        }

        public void Upgrade(bool isSelect)
        {
            if (isSelect)
            {
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
                _label.color = GameData.AssetProvider.SelectColor;
            }
            else
            {
                _icon.sprite = _model.Icon;
                _label.color = GameData.AssetProvider.DeselectColor;
            }
        }
        
        private void LocalizationSettingsOnSelectedLocaleChanged(Locale obj)
        {
            StartCoroutine(AwaitLoadText());
        }
        
        private IEnumerator AwaitLoadText()
        {
            var loadTextCommand = new LoadTextCommand(_model.Name);
            yield return loadTextCommand.Await().ContinueWith(() => _label.text = loadTextCommand.Result);
        }
    }
}