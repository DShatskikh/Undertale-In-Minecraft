using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Game
{
    public class CompanionsManager : MonoBehaviour
    {
        [SerializeField]
        private Companion _bashar;

        [SerializeField]
        private Companion _mushroom;

        [SerializeField]
        private Companion _hacker;
        
        private List<Companion> _activeCompanions = new List<Companion>();
        public List<Companion> GetAllCompanions => _activeCompanions;

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

        public void ResetAllPositions()
        {
            foreach (var companion in _activeCompanions)
            {
                companion.transform.position = GameData.CharacterController.transform.position;
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
            
            return GameData.CharacterController.transform.position;
        }
        
        private Companion GetCompanion(CompanionType companionType)
        {
            return companionType switch
            {
                CompanionType.Bashar => _bashar,
                CompanionType.Mushroom => _mushroom,
                CompanionType.Hacker => _hacker,
                _ => null
            };
        }
    }
}