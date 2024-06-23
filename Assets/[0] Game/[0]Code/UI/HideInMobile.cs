using RimuruDev;
using UnityEngine;

namespace Game
{
    public class HideInMobile : MonoBehaviour
    {
        private void Start()
        {
            if (GameData.DeviceType == CurrentDeviceType.WebMobile)
                gameObject.SetActive(false);
        }
    }
}