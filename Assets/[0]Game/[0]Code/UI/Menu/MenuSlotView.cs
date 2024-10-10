using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MenuSlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _frame, _icon;

        [SerializeField]
        private TMP_Text _label;

        private MenuSlotConfig _model;
        
        public void Init(MenuSlotConfig model)
        {
            _model = model;
            GameData.Startup.StartCoroutine(AwaitLoad());
        }

        public void Upgrade(bool isSelect)
        {
            if (isSelect)
            {
                _frame.color = GameData.AssetProvider.SelectColor;
                _label.color = GameData.AssetProvider.SelectColor;
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
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