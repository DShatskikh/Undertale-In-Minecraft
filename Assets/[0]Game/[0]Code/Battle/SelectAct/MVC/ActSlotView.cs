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
        private Image _icon, _frame;

        [SerializeField]
        private TMP_Text _label;

        public void UpdateView(ActSlotModel model)
        {
            _label.text = "???";
            GameData.Startup.StartCoroutine(AwaitTextUpgrade(model.Act.Name));
            
            if (model.IsSelected)
            {
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
                _frame.color = GameData.AssetProvider.SelectColor;
                _label.color = GameData.AssetProvider.SelectColor;
            }
            else
            {
                _icon.sprite = model.Act.Icon;
                _frame.color = GameData.AssetProvider.DeselectColor;
                _label.color = GameData.AssetProvider.DeselectColor;
            }
        }

        private IEnumerator AwaitTextUpgrade(LocalizedString text)
        {
            gameObject.SetActive(false);
            var operation = text.GetLocalizedStringAsync();
            
            while (!operation.IsDone)
                yield return null;

            _label.text = operation.Result;
            gameObject.SetActive(true);
        }
    }
}