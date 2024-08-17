using UnityEngine;
using YG;

namespace Game
{
    public class IncreaseHealth : MonoBehaviour
    {
        [SerializeField]
        private int _value = 2;
        
        public void Increase()
        {
            YandexGame.savesData.MaxHealth += _value;
            YandexGame.savesData.Health = YandexGame.savesData.MaxHealth;
        }
    }
}