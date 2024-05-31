using UnityEngine;

namespace Game
{
    public class IncreaseInt : MonoBehaviour
    {
        [SerializeField]
        private SaveKeyInt _saveKey;

        [SerializeField]
        private int _value = 1;
        
        public virtual void Use()
        {
            GameData.Saver.Save(_saveKey, GameData.Saver.LoadKey(_saveKey) + _value);
        }
    }
}