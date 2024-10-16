using RimuruDev;
using UnityEngine;

namespace Game
{
    public class HideInPC : MonoBehaviour
    {
        private void Start()
        {
            if (GameData.DeviceType == CurrentDeviceType.PC)
                gameObject.SetActive(false);
        }
    }
}