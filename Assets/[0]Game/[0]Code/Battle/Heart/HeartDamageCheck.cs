using UnityEngine;

namespace Game
{
    public class HeartDamageCheck : MonoBehaviour
    {
        [SerializeField]
        private HeartController _heartController;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Shell attack))
            {
                if (!attack.IsActive)
                    return;
                
                _heartController.Damage();
                Destroy(attack.gameObject);
            }
        }
    }
}