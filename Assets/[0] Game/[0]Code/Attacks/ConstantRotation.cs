using System;
using UnityEngine;

namespace Game
{
    public class ConstantRotation : MonoBehaviour
    {
        private void Update()
        {
            transform.Rotate(Vector3.zero);
        }
    }
}