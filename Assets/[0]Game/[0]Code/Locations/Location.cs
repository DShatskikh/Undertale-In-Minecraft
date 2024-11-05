using System;
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
    }
}