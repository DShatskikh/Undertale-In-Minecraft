using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class ExitSlotView : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Image _icon;
        
        private ExitSlotConfig _model;
        private ExitSlotViewModel _viewModel;

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
        
        public void Init(ExitSlotConfig model, ExitSlotViewModel viewModel)
        {
            _model = model;
            _viewModel = viewModel;
            StartCoroutine(AwaitLoad());
        }

        public void Upgrade(bool isSelect)
        {
            if (isSelect)
            {
                _label.color = GameData.AssetProvider.SelectColor;
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
            }
            else
            {
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