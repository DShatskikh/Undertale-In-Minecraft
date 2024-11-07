using UnityEngine;

namespace Game
{
    public class UseDialogCloseEvent : MonoBehaviour
    {
        public void Use()
        {
            EventBus.CloseDialog?.Invoke();
        }
    }
}