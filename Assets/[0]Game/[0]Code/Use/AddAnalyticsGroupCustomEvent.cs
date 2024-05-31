using System.Collections.Generic;
using UnityEngine;

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
            
            print($"Добавь аналитику Имя группы {_nameGroup} События {eventParams}");
        }
    }
}