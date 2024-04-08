using System;
using UnityEngine;

namespace Game
{
    public class CameraNoArea : MonoBehaviour
    {
        private void OnEnable()
        {
            GameData.CinemachineConfiner.m_BoundingShape2D = null;
        }
    }
}