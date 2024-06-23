using RimuruDev;
using UnityEngine;

namespace Game
{
    public class HideInPC : MonoBehaviour
    {
        private void Start()
        {
            if (GameData.DeviceType == CurrentDeviceType.WebPC)
                gameObject.SetActive(false);
        }
    }
}