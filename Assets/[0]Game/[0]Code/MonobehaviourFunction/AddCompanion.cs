using System.Linq;
using UnityEngine;
using YG;

namespace Game
{
    public class AddCompanion : MonoBehaviour
    {
        [SerializeField]
        private string _companionName;

        public void Use()
        {
            GameData.CompanionsManager.TryActivateCompanion(_companionName);
            
            if (YandexGame.savesData.Companions.Any(companion => companion == _companionName))
                return;

            YandexGame.savesData.Companions.Add(_companionName);
        }
    }
}