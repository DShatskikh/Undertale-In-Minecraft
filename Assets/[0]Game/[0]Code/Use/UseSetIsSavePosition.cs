using UnityEngine;

namespace Game
{
    public class UseSetIsSavePosition : MonoBehaviour
    {
        [SerializeField]
        private bool _isSave = true;
        
        public void Use()
        {
            GameData.SaveLoadManager.IsSave = _isSave;
        }
    }
}