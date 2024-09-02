using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SubmitButton : MonoBehaviour
    {
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnSubmit);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnSubmit);
        }

        private void OnSubmit()
        {
            EventBus.SubmitUp.Invoke();
        }
    }
}