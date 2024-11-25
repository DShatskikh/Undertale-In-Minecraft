using System;
using UnityEngine;
using UnityEngine.Analytics;

namespace Game
{
    public class LocationsManager : MonoBehaviour
    {
        private Location[] _locations;
        private Location _currentLocation;

        private void Awake()
        {
            _locations = GetComponentsInChildren<Location>(true);
            
            /*foreach (var location in _locations)
            {
                location.gameObject.SetActive(false);
            }*/
        }

        private void Start()
        {
            foreach (var location in _locations)
            {
                if (!location.gameObject.activeSelf)
                    continue;
                
                _currentLocation = location;
                break;
            }
        }

        public void SwitchLocation(string nextLocationName, int pointIndex)
        {
            foreach (var location in _locations)
            {
                if (location.GetName == nextLocationName)
                {
                    if (_currentLocation) 
                        _currentLocation.gameObject.SetActive(false);
            
                    _currentLocation = location;
                    _currentLocation.gameObject.SetActive(true);
                    
                    if (_currentLocation.Points.Length <= pointIndex)
                    {
                        Debug.LogWarning($"Такого индекса нет Всего точек: ({_currentLocation.Points.Length}) Текущий индекс: ({pointIndex}) Локация: ({_currentLocation})");
                        pointIndex = 0;
                    }
                    
                    GameData.CharacterController.transform.position = _currentLocation.Points[pointIndex].position;
                    GameData.CompanionsManager.ResetAllPositions();
                    
                    Analytics.CustomEvent("Location " + _currentLocation.gameObject.name);
                    return;
                }
                else
                {
                    location.gameObject.SetActive(false);
                }
            }
            
            Debug.LogWarning($"Нет такой локации: ({nextLocationName})");
        }

        public void CloseCurrentLocation()
        {
            if (_currentLocation) 
                _currentLocation.gameObject.SetActive(false);
        }
    }
}