using UnityEngine;

namespace Game
{
    public class ActivationAnimationTrigger : MonoBehaviour
    {
        [SerializeField] 
        private string _triggerName;
        
        public void Use()
        {
            GetComponent<Animator>().SetTrigger(_triggerName);
        }
    }
}