using System.Collections;
using UnityEngine;

namespace Game
{
    public class RotationAttack : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed;
        
        private IEnumerator Start()
        {
            while (true)
            {
                transform.Rotate(0, 0, _rotationSpeed);
                yield return null;
            }
        }
    }
}