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
            if (GameData.IsCanStartBattle)
            {
                //GameData.LocationsManager.SwitchLocation(_name);
            }
        }

        private void Start()
        {
            var funNumber = Lua.Run("return Variable[\"FUN\"]").AsInt;

            foreach (var funComponent in GetComponentsInChildren<FUN>(true))
                funComponent.gameObject.SetActive(funComponent.IsNumber(funNumber));
        }
    }
}