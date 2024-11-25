using System;
using System.Collections;
using Language.Lua;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using YG;

namespace Game
{
    public class Location : MonoBehaviour
    {
        [SerializeField]
        private string _name;
        
        [SerializeField]
        private Transform[] _points;

        public Transform[] Points => _points;
        public string GetName => _name;

        private void OnEnable()
        {
            YandexGame.savesData.LocationName = _name;
            
            if (GameData.IsCanStartBattle)
            {
                //GameData.LocationsManager.SwitchLocation(_name);
            }
        }

        private void Start()
        {
            var funNumber = Lua.Run("return Variable[\"FUN\"]").AsInt;
            print(funNumber);
            
            foreach (var funComponent in GetComponentsInChildren<FUN>(true))
            {
                if (funComponent.IsNumber(funNumber))
                    funComponent.gameObject.SetActive(true);
            }
        }
    }
}