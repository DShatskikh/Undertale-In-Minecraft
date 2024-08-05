using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class CameraAreaChange : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _event;
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out CameraAreaChecker cameraAreaChecker))
            {
                var collider = GetComponent<PolygonCollider2D>();
                
                if (GameData.CinemachineConfiner.m_BoundingShape2D != collider)
                {
                    GameData.CinemachineConfiner.m_BoundingShape2D = collider;   
                    _event?.Invoke();
                }
            }
        }
    }
}