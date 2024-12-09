using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Game
{
    public abstract class MenuButton : Button
    {
        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Image _icon;

        protected abstract Sprite Icon { get; }

        public override void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            // Ваша анимация при наведении
            PlayHoverAnimation();
        }

        public override void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            // Ваша анимация при уходе курсора
            PlayExitAnimation();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            // Ваш код обработки нажатия
            PlayClickAnimation();
        }
        
        protected void PlayHoverAnimation()
        {
            // Код для воспроизведения анимации при наведении
            _label.color = GameData.AssetProvider.SelectColor;
            _icon.sprite = GameData.AssetProvider.CharacterIcon;
        }

        protected void PlayExitAnimation()
        {
            // Код для воспроизведения анимации при уходе
            _label.color = GameData.AssetProvider.DeselectColor;
            _icon.sprite = Icon;
        }

        protected void PlayClickAnimation()
        {
        }

        protected IEnumerator AwaitLoadText(LocalizedString text)
        {
            var loadTextCommand = new LoadTextCommand(text);
            yield return loadTextCommand.Await().ContinueWith(() => _label.text = loadTextCommand.Result);
        }
    }
}