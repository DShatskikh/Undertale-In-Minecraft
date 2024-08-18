using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Game
{
    public class ActSlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _label;

        public void UpdateView(ActSlotModel model)
        {
            StartCoroutine(AwaitTextUpgrade(model.Act.Name));
            
            if (model.IsSelected)
            {
                _icon.enabled = true;
                _label.color = GameData.AssetProvider.SelectColor;
            }
            else
            {
                _icon.enabled = false;
                _label.color = GameData.AssetProvider.DeselectColor;
            }
        }

        private IEnumerator AwaitTextUpgrade(LocalizedString text)
        {
            var operation = text.GetLocalizedStringAsync();
            
            while (!operation.IsDone)
                yield return null;

            _label.text = operation.Result;
        }
    }
}