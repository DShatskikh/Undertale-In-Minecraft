using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class ResetScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _submitButton, _cancelButton;

        [SerializeField]
        private MainMenu _menu;
        
        private void OnEnable()
        {
            _submitButton.onClick.AddListener(OnSubmitClick);
            _cancelButton.onClick.AddListener(OnCancelClick);
        }

        private void OnDisable()
        {
            _submitButton.onClick.RemoveListener(OnSubmitClick);
            _cancelButton.onClick.RemoveListener(OnCancelClick);
        }

        private void OnSubmitClick()
        {
            GameData.SaveLoadManager.Reset();
        }
        
        private void OnCancelClick()
        {
            gameObject.SetActive(false);
            _menu.Activate(true);
        }
    }
}