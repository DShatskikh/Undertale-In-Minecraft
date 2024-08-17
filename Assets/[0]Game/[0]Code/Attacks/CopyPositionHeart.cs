using System;
using UnityEngine;

namespace Game
{
    public class CopyPositionHeart : MonoBehaviour
    {
        private void Start()
        {
            transform.position = GameData.HeartController.transform.position;
        }
    }
}