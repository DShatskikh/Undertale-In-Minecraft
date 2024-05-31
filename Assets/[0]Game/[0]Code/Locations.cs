using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Locations : MonoBehaviour
    {
        [SerializeField]
        private Location[] _locations;

        public IEnumerable<Location> Locations1
        {
            get => _locations;
        }
    }
}