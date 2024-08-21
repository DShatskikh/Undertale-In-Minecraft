using System;
using UnityEngine;

namespace Game
{
    public class Location : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _points;

        public Transform[] Points => _points;

        private void OnEnable()
        {
            if (GameData.IsCanStartBattle)
            {
                GameData.LocationsManager.SwitchLocation(this);
            }
        }
    }
}