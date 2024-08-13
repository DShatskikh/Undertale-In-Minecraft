using UnityEngine;
using UnityEngine.Analytics;
using YG;

namespace Game
{
    public class Location : MonoBehaviour
    {
        [SerializeField]
        private int _index;
        
        private void OnEnable()
        {
            YandexGame.savesData.LocationIndex = _index;
            GameData.TimerBeforeAdsYG.gameObject.SetActive(true);
            Analytics.CustomEvent("Location " + gameObject.name);
        }
    }
}