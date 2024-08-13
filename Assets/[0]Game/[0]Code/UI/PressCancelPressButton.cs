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
            EventBus.Cancel = () => _button.onClick.Invoke();
        }

        private void OnDisable()
        {
            EventBus.Cancel -= () => _button.onClick.Invoke();
        }
    }
}