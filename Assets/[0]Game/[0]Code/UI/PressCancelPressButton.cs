using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PressCancelPressButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        private void OnEnable()
        {
            EventBus.OnCancel = () => _button.onClick.Invoke();
        }

        private void OnDisable()
        {
            EventBus.OnCancel -= () => _button.onClick.Invoke();
        }
    }
}