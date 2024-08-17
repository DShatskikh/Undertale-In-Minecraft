using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Game
{
    public class CompanionManager : MonoBehaviour
    {
        [SerializeField]
        private Companion _bashar;

        [SerializeField]
        private Companion _mushroom;

        private List<Companion> _activeCompanions = new List<Companion>();

        private void Start()
        {
            foreach (var companion in YandexGame.savesData.Companions)
            {
                TryActivateCompanion(companion);
            }
        }

        [ContextMenu("Удалить всех компаньонов")]
        public void DeactivateAllCompanion()
        {
            foreach (var companion in _activeCompanions)
            {
                companion.gameObject.SetActive(false);
                YandexGame.savesData.Companions = new List<CompanionType>();
            }
        }
        
        public void TryActivateCompanion(CompanionType companionType)
        {
            var companion = GetCompanion(companionType);

            if (companion)
            {
                _activeCompanions.Add(companion);
                companion.gameObject.SetActive(true);
            }
        }

        public Vector2 GetNearestTarget(Companion companion)
        {
            for (int i = 0; i < _activeCompanions.Count; i++)
            {
                if (companion == _activeCompanions[i])
                {
                    if (i != 0)
                        return _activeCompanions[i - 1].transform.position;
                    
                    break;
                }
            }
            
            return GameData.Character.transform.position;
        }
        
        private Companion GetCompanion(CompanionType companionType)
        {
            switch (companionType)
            {
                case CompanionType.Bashar:
                    return _bashar;
                case CompanionType.Mushroom:
                    return _mushroom;
            }

            return null;
        }
    }
}