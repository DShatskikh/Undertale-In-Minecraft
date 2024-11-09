using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public class BattleArena : MonoBehaviour
    {
        [SerializeField]
        private Tilemap _tilemap;

        [SerializeField]
        private Transform _startPoint;

        [SerializeField]
        private GameObject _additionalObjects;
        
        public Transform StartPoint => _startPoint;
        
        private void Start()
        {
            gameObject.SetActive(false);
            _tilemap.color = _tilemap.color.SetA(0);
        }

        public void ShowAdditionalObjects(bool value)
        {
            _additionalObjects.SetActive(value);
        }
        
        public IEnumerator AwaitUpgradeA(float alpha, float duration = 1)
        {
            var progress = 0.0f;

            var startA = _tilemap.color.a;
            
            while (progress < 1)
            {
                progress += Time.deltaTime / duration;
                _tilemap.color = _tilemap.color.SetA(Mathf.Lerp(startA, alpha, progress));
                yield return null;
            }
        }
    }
}