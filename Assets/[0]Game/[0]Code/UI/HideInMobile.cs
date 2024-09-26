using RimuruDev;
using UnityEngine;

namespace Game
{
    public class HideInMobile : MonoBehaviour
    {
        private void Start()
        {
            if (GameData.DeviceType == CurrentDeviceType.Mobile)
                gameObject.SetActive(false);
        }
    }
}