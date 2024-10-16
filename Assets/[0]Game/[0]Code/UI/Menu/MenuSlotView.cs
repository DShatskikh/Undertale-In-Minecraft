using System;
using System.Collections;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Game
{
    public class MenuSlotView : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _frame, _icon;

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private MMF_Player _selectPlayer;
        
        [SerializeField]
        private MMF_Player _pressedPlayer;
        
        [SerializeField]
        private Transform _view;
        
        private MenuSlotConfig _model;
        private MenuSlotViewModel _viewModel;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.Select();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _viewModel.SubmitDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _viewModel.Use(); 
        }
        
        public void Init(MenuSlotConfig model, MenuSlotViewModel viewModel)
        {
            _model = model;
            GameData.Startup.StartCoroutine(AwaitLoad());
            _viewModel = viewModel;
            
            LocalizationSettings.SelectedLocaleChanged += LocalizationSettingsOnSelectedLocaleChanged;
        }

        private void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= LocalizationSettingsOnSelectedLocaleChanged;
        }

        private void LocalizationSettingsOnSelectedLocaleChanged(Locale obj)
        {
            GameData.Startup.StartCoroutine(AwaitLoad());
        }

        public void SubmitUp()
        {
            _view.localScale = Vector3.one;
        }
        
        public void SubmitDown()
        {
            _pressedPlayer.PlayFeedbacks();
        }
        
        public void Upgrade(bool isSelect)
        {
            if (isSelect)
            {
                _frame.color = GameData.AssetProvider.SelectColor;
                _label.color = GameData.AssetProvider.SelectColor;
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
                
                _selectPlayer.PlayFeedbacks();
            }
            else
            {
                _frame.color = GameData.AssetProvider.DeselectColor;
                _label.color = GameData.AssetProvider.DeselectColor;
                _icon.sprite = _model.Icon;
            }
        }

        private IEnumerator AwaitLoad()
        {
            var loadTextCommand = new LoadTextCommand(_model.Name);
            yield return loadTextCommand.Await().ContinueWith(() => _label.text = loadTextCommand.Result);
        }
    }
}