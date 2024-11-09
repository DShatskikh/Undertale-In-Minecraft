using System.Collections;
using UnityEngine;

namespace Game
{
    public class QueueOn : MonoBehaviour
    {
        [SerializeField]
        private float _duration;

        [SerializeField]
        private GameObject[] _gameObjects;
        
        private IEnumerator Start()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.SetActive(true);
                yield return new WaitForSeconds(_duration);
            }
        }
    }
}