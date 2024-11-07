using System;
using UnityEngine;

namespace Game
{
    public class PuzzleEnderCrystal : UseObject
    {
        [SerializeField]
        private GameObject _enderCrystal;

        [SerializeField]
        private Transform _arrow;
        
        public Action OnUse;

        public override void Use()
        {
            _arrow.gameObject.SetActive(false);
            enabled = false;
            GetComponent<Collider2D>().enabled = false;
            _enderCrystal.SetActive(true);
            OnUse?.Invoke();
        }
    }
}