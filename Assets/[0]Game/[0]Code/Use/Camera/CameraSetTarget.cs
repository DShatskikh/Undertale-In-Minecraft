using UnityEngine;

namespace Game
{
    public class CameraSetTarget : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        public void Use()
        {
            GameData.CinemachineVirtualCamera.Follow = _target;
        }
    }
}