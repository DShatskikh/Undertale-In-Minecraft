using UnityEngine;

namespace Game
{
    public class CancelUpdater : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Cancel")) 
                EventBus.Cancel?.Invoke();
            
            if (Input.GetButtonUp("Cancel")) 
                EventBus.CancelUp?.Invoke();
        }
    }
}