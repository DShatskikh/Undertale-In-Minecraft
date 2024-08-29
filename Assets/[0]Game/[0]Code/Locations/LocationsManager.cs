using System;
using UnityEngine;
using UnityEngine.Analytics;
using YG;

namespace Game
{
    public class LocationsManager : MonoBehaviour
    {
        [SerializeField]
        private Location[] _locations;

        private Location _currentLocation;

        private void Awake()
        {
            foreach (var location in _locations)
            {
                location.gameObject.SetActive(false);
            }
        }

        public void SwitchLocation(int index, int pointIndex)
        {
            if (_currentLocation) 
                _currentLocation.gameObject.SetActive(false);
            
            _currentLocation = _locations[index];
            _currentLocation.gameObject.SetActive(true);
            
            GameData.CharacterController.transform.position = _currentLocation.Points[pointIndex].position;
            GameData.CompanionsManager.ResetAllPositions();
            
            YandexGame.savesData.LocationIndex = index;
            Analytics.CustomEvent("Location " + _currentLocation.gameObject.name);
        }

        public void SwitchLocation(Location location)
        {
            _currentLocation = location;

            for (int i = 0; i < _locations.Length; i++)
            {
                if (_locations[i] == _currentLocation)
                {
                    YandexGame.savesData.LocationIndex = i;
                    break;
                }
            }
        }
    }
}