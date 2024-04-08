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
            GameData.Saver.Save();
            GameData.TimerBeforeAdsYG.gameObject.SetActive(true);
            //GameData.CinemachineConfiner.GetComponent<CinemachineVirtualCameraBase>().transform.position = GameData.Character.transform.position.SetZ(Camera.main.transform.position.z);
            
            Analytics.CustomEvent("Location " + gameObject.name);
        }
    }
}