using System;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using YG;

namespace Game
{
    public class CompanionsManager : MonoBehaviour
    {
        [SerializeField]
        private Companion[] _companions;
        
        private List<Companion> _activeCompanions = new List<Companion>();
        public List<Companion> GetAllCompanions => _activeCompanions;

        public void Register()
        {
            Lua.RegisterFunction(nameof(IsHaveCompanion), this, SymbolExtensions.GetMethodInfo(() => IsHaveCompanion(string.Empty)));
            //Load();
        }

        private void OnDestroy()
        {
            Lua.UnregisterFunction(nameof(IsHaveCompanion));
        }

        public bool IsHaveCompanion(string nameCompanion)
        {
            foreach (var companion in _activeCompanions)
            {
                if (nameCompanion == companion.GetName)
                    return true;
            }
            
            return false;
        }

        [ContextMenu("Удалить всех компаньонов")]
        public void DeactivateAllCompanion()
        {
            foreach (var companion in _activeCompanions)
            {
                companion.gameObject.SetActive(false);
            }
            
            YandexGame.savesData.Companions = new List<string>();
        }

        public void ResetAllPositions()
        {
            foreach (var companion in _activeCompanions)
            {
                companion.transform.position = GameData.CharacterController.transform.position;
            }
        }

        public void TryActivateCompanion(string companionName)
        {
            var companion = GetCompanion(companionName);

            if (companion)
            {
                _activeCompanions.Add(companion);
                
                YandexGame.savesData.Companions = new List<string>();

                foreach (var activeCompanion in _activeCompanions)
                    YandexGame.savesData.Companions.Add(activeCompanion.GetName);

                companion.gameObject.SetActive(true);
            }
        }

        public void TryDeactivateCompanion(string companionName)
        {
            var companion = GetCompanion(companionName);
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
                YandexGame.savesData.Companions.Remove(companion.GetName);
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

        public Companion GetCompanion(string companionName)
        {
            foreach (var companion in _companions)
            {
                if (companionName == companion.GetName)
                    return companion;
            }

            throw new Exception($"Not Companion: {companionName}");
        }

        public void SetMove(bool value)
        {
            foreach (var companion in _activeCompanions) 
                companion.SetMove(value);
        }

        private void Load()
        {
            foreach (var companionName in YandexGame.savesData.Companions)
            {
                var companion = GetCompanion(companionName);

                if (companion)
                {
                    _activeCompanions.Add(companion);
                    companion.gameObject.SetActive(true);
                }
            }
        }
    }
}