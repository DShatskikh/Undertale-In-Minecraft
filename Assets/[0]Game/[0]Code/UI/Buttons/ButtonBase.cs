using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public abstract class ButtonBase : MonoBehaviour
    {
        protected Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public abstract void OnClick();
    }
}