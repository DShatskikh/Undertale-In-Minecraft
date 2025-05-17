using UnityEngine;

namespace Game
{
    public class WriteBool : MonoBehaviour
    {
        [SerializeField]
        private SaveKeyBool _saveKey;

        [SerializeField]
        private bool _value = true;
        
        public void Use()
        {
            GameData.Saver.Save(_saveKey, _value);
        }
    }
}