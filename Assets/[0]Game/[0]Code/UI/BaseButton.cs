using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public abstract class BaseButton : MonoBehaviour
    {
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();
    }
}