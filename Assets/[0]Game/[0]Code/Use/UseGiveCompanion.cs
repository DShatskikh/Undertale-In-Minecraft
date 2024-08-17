using System.Linq;
using UnityEngine;
using YG;

namespace Game
{
    public class UseGiveCompanion : MonoBehaviour
    {
        [SerializeField]
        private CompanionType _companion;

        public void Use()
        {
            GameData.CompanionManager.TryActivateCompanion(_companion);
            
            if (YandexGame.savesData.Companions.Any(companion => companion == _companion))
                return;

            YandexGame.savesData.Companions.Add(_companion);
        }
    }
}