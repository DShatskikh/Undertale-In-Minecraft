using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using YG;

namespace Game
{
    public class UseGoldTulipMonolog : UseObject
    {
        [SerializeField]
        private LocalizedString _localizedString;
            
        [SerializeField] 
        private UnityEvent _endEvent;

        public override void Use()
        {
            _localizedString.Arguments = new object[] { 4 - YandexGame.savesData.GoldTulip };
            GameData.Monolog.Show(new[] {_localizedString});
            EventBus.OnCloseMonolog += _endEvent.Invoke;
        }
    }
}