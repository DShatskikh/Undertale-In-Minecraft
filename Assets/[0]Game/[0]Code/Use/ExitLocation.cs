using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class ExitLocation : MonoBehaviour
    {
        [SerializeField] 
        private Transform _point;

        [SerializeField]
        private GameObject _exitLocation, _nextLocation;
        
        public void Exit()
        {
            _exitLocation.SetActive(false);
            GameData.Character.transform.position = _point.position;
            _nextLocation.SetActive(true);
        }
    }
}