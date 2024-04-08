using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Game
{
    public class UseAds : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _event;
        
        public void Use()
        {
            YandexGame.RewardVideoEvent += RewardVideoEvent;
            YandexGame.RewVideoShow(1);
        }

        private void RewardVideoEvent(int obj)
        {
            YandexGame.RewardVideoEvent = null;
            
            if (obj == 1)
                _event.Invoke();
            else
                Debug.LogError("Не то число ????");
        }
    }
}