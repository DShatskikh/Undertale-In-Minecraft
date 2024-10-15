using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class DropdownItem : MonoBehaviour, IPointerEnterHandler,IPointerUpHandler
    {
        [SerializeField]
        private TMP_Text _label;

        private LanguageDropdown _viewModel;
        private int _index;

        private void OnEnable()
        {
            _viewModel = LanguageDropdown.GetInstance;
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

        public void Select(bool value)
        {
            _label.color = value ? GameData.AssetProvider.SelectColor : GameData.AssetProvider.DeselectColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _viewModel.SelectSlot(_index);
            print($"OnPointerEnter: {_index}");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _viewModel.Activate(false);
        }
    }
}