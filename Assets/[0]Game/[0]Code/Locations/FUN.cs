using UnityEngine;

namespace Game
{
    public class FUN : MonoBehaviour
    {
        [SerializeField]
        private int _min;

        [SerializeField]
        private int _max;
        
        public bool IsNumber(int funNumber) => 
            funNumber >= _min && funNumber <= _max;
    }
}