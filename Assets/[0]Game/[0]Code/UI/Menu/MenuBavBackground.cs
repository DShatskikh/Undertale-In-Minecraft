using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class MenuBavBackground : MonoBehaviour
    {
        [SerializeField]
        private GameObject _passed;
        
        [SerializeField]
        private GameObject _good;
        
        [SerializeField]
        private GameObject _bad;
        
        [SerializeField]
        private GameObject _strange;
        
        private void Start()
        {
            _passed.SetActive(Lua.IsTrue("IsPassedEnding() == true"));
            _good.SetActive(Lua.IsTrue("Variable[\"IsBavGoodEnding\"] == true"));
            _bad.SetActive(Lua.IsTrue("Variable[\"IsBavBadEnding\"] == true"));
            _strange.SetActive(Lua.IsTrue("Variable[\"IsBavStrangeEnding\"] == true"));
        }
    }
}