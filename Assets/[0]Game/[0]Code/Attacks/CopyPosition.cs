using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class CopyPosition : MonoBehaviour
    {
        [SerializeField] 
        private Transform _target;
        
        private void Start()
        {
            transform.position = _target.position;
        }
    }
}