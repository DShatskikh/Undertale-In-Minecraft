using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Game
{
    public class UseAdsDialog : UseObject
    {
        [SerializeField]
        private Replica _replica;
            
        [SerializeField] 
        private UnityEvent _endEvent;

        private const int StartView = 942535 + 1;
        
        public override void Use()
        {
            _replica.LocalizationString.Arguments = new object[] { StartView - YandexGame.savesData.AdsViews };
            GameData.Dialog.Show(new[] {_replica});
            EventBus.CloseDialog += _endEvent.Invoke;
        }
    }
}