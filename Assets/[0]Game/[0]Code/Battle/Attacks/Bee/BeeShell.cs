using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class BeeShell : Shell
    {
        private float _progress;
        private float _startY;
        private float _speed;

        private void Start()
        {
            Init(Random.Range(0, 2) == 1 ? 1 : -1);
        }

        public void Init(float speed)
        {
            _startY = transform.position.y;
            _speed = speed;
            gameObject.SetActive(true);
        }
        
        private void Update()
        {
            transform.position = transform.position
                .AddX(Time.deltaTime * 2)
                .SetY(_startY + (float)Math.Sin(_progress));

            _progress += Time.deltaTime * 2 * _speed;
        }
    }
}