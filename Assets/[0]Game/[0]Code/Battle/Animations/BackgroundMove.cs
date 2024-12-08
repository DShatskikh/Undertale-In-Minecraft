using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BackgroundMove : MonoBehaviour
    {
        [SerializeField] private RawImage _lines1, _lines2;
        [SerializeField] private Vector2 _direction1;
        [SerializeField] private Vector2 _direction2;

        private void Update()
        {
            MoveUV(_lines1, _direction1);
            MoveUV(_lines2, _direction2);
        }

        private void MoveUV(RawImage rawImage, Vector2 direction)
        {
            var uv = rawImage.uvRect;
            uv.x += direction.x * Time.deltaTime;
            uv.y += direction.y * Time.deltaTime;
            rawImage.uvRect = uv;
        }
    }
}