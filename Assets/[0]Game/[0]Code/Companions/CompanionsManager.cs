using System;
using System.Collections.Generic;
using System.Linq;
using PixelCrushers.DialogueSystem;
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
            Lua.RegisterFunction(nameof(IsHaveCompanion), this, SymbolExtensions.GetMethodInfo(() => IsHaveCompanion(string.Empty)));
            
            foreach (var companion in YandexGame.savesData.Companions)
            {
                TryActivateCompanion(companion);
            }
        }

        private void OnDestroy()
        {
            Lua.UnregisterFunction(nameof(IsHaveCompanion));
        }

        private bool IsHaveCompanion(string nameCompanion) => 
            YandexGame.savesData.Companions.Any(companion => nameCompanion == Enum.GetName(typeof(Companion), companion));

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
        
        public void TryDeactivateCompanion(CompanionType companionType)
        {
            var companion = GetCompanion(companionType);
            var isHave = false;
            
            foreach (var activeCompanion in _activeCompanions)
            {
                if (activeCompanion == companion)
                {
                    isHave = true;
                    break;
                }
            }

            if (!isHave)
                return;
            
            if (companion)
            {
                _activeCompanions.Remove(companion);
                companion.gameObject.SetActive(false);
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
        
        public Companion GetCompanion(CompanionType companionType)
        {
            return companionType switch
            {
                CompanionType.Bashar => _bashar,
                CompanionType.Mushroom => _mushroom,
                CompanionType.Hacker => _hacker,
                _ => null
            };
        }

        public void SetMove(bool value)
        {
            foreach (var companion in _activeCompanions) 
                companion.SetMove(value);
        }
    }
}