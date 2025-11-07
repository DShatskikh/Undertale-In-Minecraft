using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public sealed class HerobrineBed : UseObject
    {
        [SerializeField]
        private LocalizedString[] _replica_normal;
        
        [SerializeField]
        private LocalizedString[] _replica_noneItem;
        
        public override void Use()
        {
            if (YG.YandexGame.savesData.IsNoneItem)
            {
                GameData.Monolog.Show(_replica_noneItem);
            }
            else
            {
                GameData.Monolog.Show(_replica_normal);
            }
        }
    }
}