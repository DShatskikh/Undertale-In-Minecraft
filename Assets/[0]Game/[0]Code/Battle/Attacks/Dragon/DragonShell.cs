using UnityEngine;

namespace Game
{
    public class DragonShell : Shell
    {
        private float _directionX;

        public void SetDirection(float directionX)
        {
            GetComponent<SpriteRenderer>().flipX = directionX < 0f;
            _directionX = directionX;
        }
        
        private void Update()
        {
            transform.position += transform.right * _directionX * Time.deltaTime * transform.localScale.x * 2.5f;
        }
    }
}