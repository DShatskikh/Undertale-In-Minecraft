using System;
using UnityEngine;

namespace Game
{
    public class SizeDeltaYUpgrade : MonoBehaviour
    {
        [SerializeField]
        private int _y = 116;
        
        private void Start()
        {
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, _y);
        }
    }
}