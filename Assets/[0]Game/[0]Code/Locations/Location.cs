using System.Collections;
using UnityEngine;

namespace Game
{
    public class Location : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _points;

        public Transform[] Points => _points;
    }
}