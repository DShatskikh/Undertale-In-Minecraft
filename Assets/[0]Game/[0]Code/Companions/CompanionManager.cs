using UnityEngine;
using YG;

namespace Game
{
    public class CompanionManager : MonoBehaviour
    {
        [SerializeField]
        private Companion _bashar;

        private void Start()
        {
            foreach (var companion in YandexGame.savesData.Companions)
            {
                GiveCompanion(companion);
            }
        }

        public void GiveCompanion(CompanionType companionType)
        {
            if (companionType == CompanionType.Bashar) 
                _bashar.gameObject.SetActive(true);
        }
    }
}