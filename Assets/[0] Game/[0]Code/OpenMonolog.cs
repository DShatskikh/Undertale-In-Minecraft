using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class OpenMonolog : MonoBehaviour
    {
        [SerializeField, TextArea] 
        private string[] _texts;

        [SerializeField] 
        private UnityEvent _endEvent;
        
        public void Open()
        {
            GameData.Monolog.Show(_texts);
            EventBus.OnCloseMonolog += _endEvent.Invoke;
        }
    }
}