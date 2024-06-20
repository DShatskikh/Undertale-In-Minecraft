using UnityEngine;
using UnityEngine.Events;

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
            _replica.LocalizationString.Arguments = new object[] { StartView - GameData.AdsViews };
            GameData.Dialog.SetReplicas(new[] {_replica});
            EventBus.OnCloseDialog += _endEvent.Invoke;
        }
    }
}