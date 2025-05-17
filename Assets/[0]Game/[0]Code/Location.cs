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
            GameData.Saver.SaveAll();
            Analytics.CustomEvent("Location " + gameObject.name);
        }
    }
}