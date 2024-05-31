using UnityEngine;

namespace Game
{
    public class AddAnalyticsCustomEvent : MonoBehaviour
    {
        [SerializeField]
        private string _nameEvent;

        public void Use()
        {
            print("Добавь аналитику " + _nameEvent);
        }
    }
}