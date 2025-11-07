using UnityEngine;
using UnityEngine.Analytics;
using YG;

namespace Game
{
    public class Location : MonoBehaviour
    {
        [SerializeField]
        private int _index;
        
        [SerializeField]
        private string _id;

        public string GetID => _id;

        private void OnEnable()
        {
            YandexGame.savesData.LocationIndex = _index;
            YandexGame.savesData.LocationID = _id;
            GameData.TimerBeforeAdsYG.gameObject.SetActive(true);
            Analytics.CustomEvent("Location " + gameObject.name);
        }
    }
}