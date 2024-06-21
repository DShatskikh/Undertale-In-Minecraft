using Cinemachine;
using Super_Auto_Mobs;
using UnityEngine;
using UnityEngine.Analytics;

namespace Game
{
    public class Location : MonoBehaviour
    {
        [SerializeField]
        private int _index;
        
        private void OnEnable()
        {
            GameData.LocationIndex = _index;
            GameData.TimerBeforeAdsYG.gameObject.SetActive(true);
            Analytics.CustomEvent("Location " + gameObject.name);
        }
    }
}