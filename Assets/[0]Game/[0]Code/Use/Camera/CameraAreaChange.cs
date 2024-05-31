using UnityEngine;

namespace Game
{
    public class CameraAreaChange : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out CameraAreaChecker cameraAreaChecker))
            {
                if (GameData.CinemachineConfiner.m_BoundingShape2D != GetComponent<PolygonCollider2D>())
                    GameData.CinemachineConfiner.m_BoundingShape2D = GetComponent<PolygonCollider2D>();
            }
        }
    }
}