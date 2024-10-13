using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GuideSlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _label;

        private GuideConfig _model;
        
        public void Init(GuideConfig model)
        {
            //_icon.sprite = model.Config.Icon;
            _model = model;
            _icon.sprite = _model.Icon;
            StartCoroutine(AwaitLoadText());
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
        
        private IEnumerator AwaitLoadText()
        {
            var loadTextCommand = new LoadTextCommand(_model.Name);
            yield return loadTextCommand.Await().ContinueWith(() => _label.text = loadTextCommand.Result);
        }
    }
}