using System.Collections;
using Super_Auto_Mobs;
using UnityEngine;

namespace Game
{
    public class MoveSin : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 3;
        
        [SerializeField]
        private float _ySpeed = 5;
        
        [SerializeField]
        private float _amplitude = 1;
        
        private IEnumerator Start()
        {
            var y = 0f;
            var startPosition = transform.localPosition;

            while (true)
            {
                y += Time.deltaTime;
                transform.localPosition = transform.localPosition
                    .AddX(-_moveSpeed * Time.deltaTime)
                    .SetY(Mathf.Sin(y * _ySpeed) * _amplitude + startPosition.y);
                yield return null;
            }
        }
    }
}