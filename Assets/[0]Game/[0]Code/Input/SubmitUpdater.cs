using UnityEngine;

namespace Game
{
    public class SubmitUpdater : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(1))
            {
                EventBus.OnSubmit?.Invoke();
            }
        }
    }
}