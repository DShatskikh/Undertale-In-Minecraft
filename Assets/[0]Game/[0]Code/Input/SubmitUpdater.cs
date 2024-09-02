using UnityEngine;

namespace Game
{
    public class SubmitUpdater : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Submit")) 
                EventBus.Submit?.Invoke();

            if (Input.GetButtonUp("Submit")) 
                EventBus.SubmitUp?.Invoke();
        }
    }
}