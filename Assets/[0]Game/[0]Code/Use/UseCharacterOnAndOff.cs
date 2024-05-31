using UnityEngine;

namespace Game
{
    public class UseCharacterOnAndOff : MonoBehaviour
    {
        [SerializeField]
        private bool _isValue;
        
        public void Use()
        {
            GameData.Character.enabled = _isValue;
            GameData.Character.GetComponent<Collider2D>().enabled = _isValue;
        }
    }
}