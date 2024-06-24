using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PressSubmitPressButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        private void OnEnable()
        {
            EventBus.OnSubmit = () => _button.onClick.Invoke();
        }

        private void OnDisable()
        {
            EventBus.OnSubmit -= () => _button.onClick.Invoke();
        }
    }
}