using UnityEngine;
using UnityEngine.Localization;
using YG;

namespace Game
{
    public sealed class Herobrine_Door : UseObject
    {
        [SerializeField]
        private LocalizedString[] _normal;

        [SerializeField]
        private UseSelect _useSelect;
        
        public override void Use()
        {
            if (YandexGame.savesData.IsOneOrMoreEnd)
            {
                YandexGame.savesData.IsEscapeHomeHerobrine = true;
                _useSelect.Use();
            }
            else
            {
                GameData.Monolog.Show(_normal);
            }
        }
    }
}