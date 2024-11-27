using System;
using PixelCrushers;
using UnityEngine;
using UnityEngine.Analytics;

namespace Game
{
    public class LocationsManager : Saver
    {
        private Location[] _locations;
        private Location _currentLocation;
        private Data _saveData = new();
        
        [Serializable]
        public class Data
        {
            public string LocationName = "BavWorld";
            public int PointIndex;
        }
        
        public override void Awake()
        {
            base.Awake();
            _locations = GetComponentsInChildren<Location>(true);
            
            foreach (var location in _locations) 
                location.gameObject.SetActive(false);
        }

        public override string RecordData()
        {
            return SaveSystem.Serialize(_saveData);
        }

        public override void ApplyData(string s)
        {
            var data = SaveSystem.Deserialize(s, _saveData);
            _saveData = data;

            SwitchLocation(data.LocationName, data.PointIndex);
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
                        Debug.LogWarning($"Такого индекса нет Всего точек: ({_currentLocation.Points.Length}) Текущий индекс: ({pointIndex}) Локация: ({nextLocationName})");
                        pointIndex = 0;
                    }
                    
                    GameData.CharacterController.transform.position = _currentLocation.Points[pointIndex].position;
                    GameData.CinemachineVirtualCamera.ForceCameraPosition(GameData.CharacterController.transform.position, Quaternion.identity);

                    GameData.CompanionsManager.ResetAllPositions();
                    
                    Analytics.CustomEvent("Location " + _currentLocation.gameObject.name);
                    _saveData.LocationName = nextLocationName;
                    _saveData.PointIndex = pointIndex;
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