using System;
using UnityEngine;

namespace Game
{
    public class EnderPost : MonoBehaviour
    {
        [SerializeField]
        private GameObject _crystal;

        private bool _isActiveCrystal;
        
        public bool IsActiveCrystal => _isActiveCrystal;

        public void ActivateCrystal()
        {
            _crystal.SetActive(true);
            _isActiveCrystal = true;
        }

        public void DeactivateCrystal()
        {
            _crystal.SetActive(false);
            _isActiveCrystal = false;
        }
    }
}