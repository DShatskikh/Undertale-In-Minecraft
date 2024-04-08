using UnityEngine;

namespace Game
{
    public class IncreaseHealth : MonoBehaviour
    {
        [SerializeField]
        private int _value = 2;
        
        public void Increase()
        {
            GameData.MaxHealth += _value;
            GameData.Health = GameData.MaxHealth;
        }
    }
}