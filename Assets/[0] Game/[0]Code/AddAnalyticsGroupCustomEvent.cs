using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Game
{
    public class AddAnalyticsGroupCustomEvent : MonoBehaviour
    {
        [SerializeField]
        private string _nameGroup, _nameEvent;

        public void Use()
        {
            var eventParams = new Dictionary<string, string>
            {
                { _nameGroup, _nameEvent }
            };
            
            YandexMetrica.Send(_nameGroup, eventParams);
        }
    }
}