using UnityEngine;
using YG;

namespace Game
{
    public class AddAnalyticsCustomEvent : MonoBehaviour
    {
        [SerializeField]
        private string _nameEvent;

        public void Use()
        {
            YandexMetrica.Send(_nameEvent);
        }
    }
}