using UnityEngine;

namespace Game
{
    public class BavShop : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _trader;
        
        private void Update()
        {
            if (Vector2.Distance(GameData.CharacterController.transform.position, transform.position) < 5)
            {
                _trader.flipX = GameData.CharacterController.transform.position.x - transform.position.x < 0;
            }
            else
            {
                _trader.flipX = true;
            }
        }
    }
}