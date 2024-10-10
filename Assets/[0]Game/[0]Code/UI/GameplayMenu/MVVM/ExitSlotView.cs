using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ExitSlotView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Image _icon;
        
        private ExitSlotConfig _model;
        
        public void Init(ExitSlotConfig model)
        {
            _model = model;
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