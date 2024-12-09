using UnityEngine;

namespace Game
{
    public class CameraResetTarget : MonoBehaviour
    {
        public void Use()
        {
            GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.transform;
        }
    }
}